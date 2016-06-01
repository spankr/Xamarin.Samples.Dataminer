using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Sample.Dataminer.Logic.Models;

namespace Xamarin.Sample.Dataminer.Logic
{
    public class SampleProjectService
    {

        public IList<SampleProject> GetSampleProjects(string startingPath)
        {
            var projects = new List<SampleProject>();

            var fileNames = FindAllMetadataFiles(startingPath);

            foreach (var name in fileNames)
            {
                var p = new SampleProject();
                p.Name = name.Replace(startingPath, "").Replace(@"\Metadata.xml", "");
                try
                {
                    p.Metadata = ReadMetadataFile(name);
                }
                catch
                {
                    // We're okay if it doesn't parse. Its just not a proper project.
                }
                projects.Add(p);
            }

            return projects;
        }
        /// <summary>
        /// Traverse a directory and find all the metadata files
        /// </summary>
        /// <see cref="https://msdn.microsoft.com/en-us/library/07wt70x2.aspx"/>
        /// <param name="startingPath">Root path to start looking</param>
        /// <returns>Collection of filenames</returns>
        internal IList<string> FindAllMetadataFiles(string startingPath)
        {
            var files = new List<string>();
            if (Directory.Exists(startingPath))
            {
                var fileEntries = Directory.GetFiles(startingPath, "Metadata.xml", SearchOption.AllDirectories);
                files.AddRange(fileEntries);
            }
            else
            {
                throw new FileNotFoundException($"{startingPath} is not a valid path.");
            }

            return files;
        }

        internal IDictionary<string, Metadata> BuildMetaDictionary(IEnumerable<string> fileNames)
        {
            var dict = new Dictionary<string, Metadata>();

            foreach (var name in fileNames)
            {
                dict[name] = ReadMetadataFile(name);
            }

            return dict;
        }

        /// <summary>
        /// Read a file in as a Metadata object
        /// </summary>
        /// <param name="name"></param>
        /// <returns>Metadata object</returns>
        internal static Metadata ReadMetadataFile(string name)
        {
            using (var stream = new StreamReader(name))
            {
                try
                {
                    var metadata = MetadataMarshaller.Marshal(stream);
                    return metadata;
                }
                catch (InvalidOperationException e)
                {
                    throw new Exception($"Unable to marshal {name}", e);
                }
            }
        }
    }
}
