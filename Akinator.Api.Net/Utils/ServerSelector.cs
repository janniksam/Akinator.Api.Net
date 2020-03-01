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
                        new Server(ServerType.Person, "srv2.akinator.com:9306/ws"),
                        new Server(ServerType.Object, "srv14.akinator.com:9293/ws"),
                        new Server(ServerType.Animal, "srv13.akinator.com:9287/ws")
                    }
                },
                {
                    Language.Arabic, new[]
                    {
                        new Server(ServerType.Person, "srv2.akinator.com:9155/ws")
                    }
                },
                {
                    Language.Italian, new[]
                    {
                        new Server(ServerType.Person, "srv9.akinator.com:9214/ws"),
                        new Server(ServerType.Animal, "srv9.akinator.com:9261/ws")
                    }
                },
                {
                    Language.Spanish, new[]
                    {
                        new Server(ServerType.Person, "srv13.akinator.com:9194/ws"),
                        new Server(ServerType.Animal, "srv13.akinator.com:9257/ws")
                    }
                },
                {
                    Language.French, new[]
                    {
                        new Server(ServerType.Person, "srv3.akinator.com:9217/ws"),
                        new Server(ServerType.Object, "srv3.akinator.com:9218/ws"),
                        new Server(ServerType.Animal, "srv3.akinator.com:9259/ws")
                    }
                },
                {
                    Language.Russian, new[]
                    {
                        new Server(ServerType.Person, "srv12.akinator.com:9190/ws")
                    }
                }
            };

        public static string GetServerFor(Language language, ServerType type) => 
            m_languageServerMapping[language].FirstOrDefault(p => p.Type == type)?.Url;
    }
}
