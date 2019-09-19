using System;
using System.IO;
using System.Xml;

namespace Akinator.Api.Net.Utils
{
    public static class XmlConverter
    {
        public static T ToClass<T>(string data)
        {
            var response = default(T);

            if (!string.IsNullOrEmpty(data))
            {
                var settings = new XmlReaderSettings
                {
                    IgnoreWhitespace = true
                };
                var serializer = CachedXmlSerializerFactory.Create(typeof(T));
                var reader = XmlReader.Create(new StringReader(data), settings);
                response = (T)Convert.ChangeType(serializer.Deserialize(reader), typeof(T));
            }
            return response;
        }
    }
}