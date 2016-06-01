using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Xamarin.Sample.Dataminer.Logic.Models;

namespace Xamarin.Sample.Dataminer.Logic
{
    public static class MetadataMarshaller
    {
        public static Metadata Marshal(StreamReader content)
        {
            Metadata result;
            XmlSerializer serializer = new XmlSerializer(typeof(Metadata));
            result = (Metadata)serializer.Deserialize(content);

            return result;
        }
    }
}
