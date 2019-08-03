using System.Collections.Generic;
using System.Linq;
using Akinator.Api.Net.Enumerations;
using Akinator.Api.Net.Model;

namespace Akinator.Api.Net.Utils
{
    public static class ServerSelector
    {
        // todo Add more languages...
        private static readonly IReadOnlyDictionary<Language, Server[]> m_languageServerMapping =
            new Dictionary<Language, Server[]>()
            {
                {
                    Language.German, new[]
                    {
                        new Server(ServerType.Person, "srv14.akinator.com:9283/ws"),
                        new Server(ServerType.Animal, "srv14.akinator.com:9284/ws")
                    }
                },
                {
                    Language.English, new[]
                    {
                        new Server(ServerType.Person, "srv13.akinator.com:9196/ws")
                    }
                }
            };

        public static string GetServerFor(Language language, ServerType type) => 
            m_languageServerMapping[language].FirstOrDefault(p => p.Type == type)?.Url;
    }
}