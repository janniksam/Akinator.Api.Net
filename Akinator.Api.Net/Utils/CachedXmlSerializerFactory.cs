using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Akinator.Api.Net.Utils
{
    public static class CachedXmlSerializerFactory
    {
        private static readonly object m_syncRootCache = new object();
        private static readonly Dictionary<Type, XmlSerializer> m_cache = new Dictionary<Type, XmlSerializer>();
        
        public static XmlSerializer Create(Type type)
        {
            XmlSerializer serializer;
            lock (m_syncRootCache)
            {
                if (m_cache.TryGetValue(type, out serializer))
                {
                    return serializer;
                }
            }
            lock (type)
            {
                serializer = XmlSerializer.FromTypes(new[] { type })[0];
            }
            lock (m_syncRootCache)
            {
                m_cache[type] = serializer;
            }

            return serializer;
        }
    }
}
