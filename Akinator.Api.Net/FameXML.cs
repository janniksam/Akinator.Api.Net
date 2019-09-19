using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace Akinator.Api.Net
{
    public class FameXML
    {
        [Serializable, XmlRoot("AWARD")]
        public class AWARD
        {
            [XmlElement(ElementName = "TYPE")]
            public string Type { get; set; }
            [XmlElement(ElementName = "AWARD_ID")]
            public string Award_ID { get; set; }
            [XmlElement(ElementName = "NOM")]
            public string Character_Name { get; set; }
            [XmlElement(ElementName = "DESCRIPTION")]
            public string Description { get; set; }
            [XmlElement(ElementName = "PSEUDO")]
            public string Winner_Name { get; set; }
            [XmlElement(ElementName = "POS")]
            public string POS { get; set; }
            [XmlElement(ElementName = "DELAI")]
            public string DELAI { get; set; }
        }

        [Serializable, XmlRoot("AWARDS")]
        public class AWARDS
        {
            [XmlElement(ElementName = "AWARD")]
            public List<AWARD> AWARD { get; set; }
        }

        [Serializable, XmlRoot("RESULT")]
        public class RESULT
        {
            [XmlElement(ElementName = "COMPLETION")]
            public string COMPLETION { get; set; }
            [XmlElement(ElementName = "NB_AWARDS")]
            public string NB_AWARDS { get; set; }
            [XmlElement(ElementName = "AWARDS")]
            public AWARDS AWARDS { get; set; }
        }

        public static class XmlConverter
        {
            public static T ToClass<T>(string data)
            {
                var response = default(T);

                if (!string.IsNullOrEmpty(data))
                {
                    var settings = new XmlReaderSettings() { IgnoreWhitespace = true };
                    var serializer = XmlSerializerFactoryNoThrow.Create(typeof(T));
                    var reader = XmlReader.Create(new StringReader(data), settings);
                    response = (T)Convert.ChangeType(serializer.Deserialize(reader), typeof(T));
                }
                return response;
            }
        }

        public static class XmlSerializerFactoryNoThrow
        {
            public static Dictionary<Type, XmlSerializer> cache = new Dictionary<Type, XmlSerializer>();

            private static object SyncRootCache = new object();

            public static XmlSerializer Create(Type type)
            {
                XmlSerializer serializer;

                lock (SyncRootCache)
                    if (cache.TryGetValue(type, out serializer))
                        return serializer;

                lock (type)
                {
                    serializer = XmlSerializer.FromTypes(new[] { type })[0];
                }

                lock (SyncRootCache) cache[type] = serializer;
                return serializer;
            }
        }
    }
}
