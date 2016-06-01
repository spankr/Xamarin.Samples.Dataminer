using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Xamarin.Sample.Dataminer.Logic
{
    [XmlRoot("SampleMetadata")]
    public class Metadata
    {
        public Guid ID { get; set; }
        public bool Gallery { get; set; }
        public bool HideZip { get; set; }
        public bool IsFullApplication { get; set; }
        public string Level { get; set; }
        public string LicenseRequirement { get; set; }
        public string Brief { get; set; }
        public string SupportedPlatforms { get; set; }
        public string Tags { get; set; }

        public IEnumerable<string> TagsCollection
        {
            get
            {
                return Tags.Split(',').Select(t => t.Trim());
            }
        }

        public IEnumerable<string> SupportedPlatformsCollection
        {
            get
            {
                return SupportedPlatforms.Split(',').Select(t => t.Trim());
            }
        }
    }
}