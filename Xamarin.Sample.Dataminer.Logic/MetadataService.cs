using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xamarin.Sample.Dataminer.Logic
{
    public class MetadataService
    {
        /// <summary>
        /// Traverse a directory and find all the metadata files
        /// </summary>
        /// <see cref="https://msdn.microsoft.com/en-us/library/07wt70x2.aspx"/>
        /// <param name="startingPath">Root path to start looking</param>
        /// <returns>Collection of filenames</returns>
        public IList<string> FindAllMetadataFiles(string startingPath)
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

        public IDictionary<string, Metadata> BuildMetaDictionary(IEnumerable<string> fileNames)
        {
            var dict = new Dictionary<string, Metadata>();

            foreach(var name in fileNames)
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
        protected static Metadata ReadMetadataFile(string name)
        {
            using (var stream = new StreamReader(name))
            {
                var metadata = MetadataMarshaller.Marshal(stream);
                return metadata;
            }
        }
    }
}
