using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace Xamarin.Sample.Dataminer.Logic.Tests
{
    public class MetadataParsingTests
    {
        /// <summary>
        /// <see cref="http://xunit.github.io/docs/capturing-output.html"/>
        /// </summary>
        private readonly ITestOutputHelper output;

        public MetadataParsingTests(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void ParseMarshallMetadata()
        {
            using (var stream = new StreamReader("Resources/Metadata.xml"))
            {
                Assert.NotNull(stream);
                var metadata = MetadataMarshaller.Marshal(stream);

                Assert.NotNull(metadata);
                Assert.Equal(Guid.Parse("71D70AA5-FE69-4934-8FDB-3486D33155FC"), metadata.ID);
                Assert.True(metadata.IsFullApplication, "Not a full application");
                Assert.Equal("Beginner", metadata.Level);
                Assert.Contains("User Interface", metadata.Tags);
                Assert.Contains("Getting Started", metadata.Tags);

                Assert.Contains("User Interface", metadata.TagsCollection);
                Assert.Contains("Getting Started", metadata.TagsCollection);

                Assert.Contains("Android", metadata.SupportedPlatforms);
                Assert.Contains("iOS", metadata.SupportedPlatforms);
                Assert.Contains("Windows", metadata.SupportedPlatforms);

                Assert.Contains("Android", metadata.SupportedPlatformsCollection);
                Assert.Contains("iOS", metadata.SupportedPlatformsCollection);
                Assert.Contains("Windows", metadata.SupportedPlatformsCollection);

                Assert.Equal("Starter", metadata.LicenseRequirement);
                Assert.Equal("Demonstrates a cross-platform app using Xamarin.Forms that retrieves data from a weather service.", metadata.Brief);
                Assert.True(metadata.Gallery, "Not a gallery");
                Assert.False(metadata.HideZip, "Zip is not hidden");
            }
        }

        [Theory]
        [InlineData("Resources/SampleProjPath")]
        public void FindAllMetadataFileNames(string goodPath)
        {
            var svc = new SampleProjectService();

            var filenames = svc.FindAllMetadataFiles(goodPath);
            Assert.Equal(2, filenames.Count(f => f.EndsWith("Metadata.xml")));

            var dict = svc.BuildMetaDictionary(filenames);

            Assert.Equal(2, dict.Count);
            Assert.Equal(filenames, new List<string>(dict.Keys));
        }

        [Theory]
        [InlineData("Resources/XXX")]
        public void FindAllMetadataFiles_InvalidPath(string badPath)
        {
            var svc = new SampleProjectService();

            Assert.Throws<FileNotFoundException>(() => svc.FindAllMetadataFiles(badPath));
        }
    }
}
