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
                        new Server(ServerType.Person, "srv14.akinator.com:9369/ws"),
                        new Server(ServerType.Animal, "srv14.akinator.com:9370/ws")
                    }
                },
                {
                    Language.English, new[]
                    {
                        new Server(ServerType.Person, "srv2.akinator.com:9317/ws"),
                        new Server(ServerType.Object, "srv2.akinator.com:9319/ws"),
                        new Server(ServerType.Animal, "srv2.akinator.com:9318/ws"),
                        new Server(ServerType.Movie, "srv13.akinator.com:9364/ws")
                    }
                },
                {
                    Language.Arabic, new[]
                    {
                        new Server(ServerType.Person, "srv2.akinator.com:9315/ws")
                    }
                },
                {
                    Language.Italian, new[]
                    {
                        new Server(ServerType.Person, "srv9.akinator.com:9380/ws"),
                        new Server(ServerType.Animal, "srv9.akinator.com:9383/ws")
                    }
                },
                {
                    Language.Spanish, new[]
                    {
                        new Server(ServerType.Person, "srv6.akinator.com:9354/ws"),
                        new Server(ServerType.Animal, "srv13.akinator.com:9362/ws")
                    }
                },
                {
                    Language.French, new[]
                    {
                        new Server(ServerType.Person, "srv3.akinator.com:9331/ws"),
                        new Server(ServerType.Object, "srv3.akinator.com:9330/ws"),
                        new Server(ServerType.Animal, "srv3.akinator.com:9329/ws"),
                        new Server(ServerType.Movie, "srv13.akinator.com:9387/ws")
                    }
                },
                {
                    Language.Russian, new[]
                    {
                        new Server(ServerType.Person, "srv12.akinator.com:9340/ws")
                    }
                }
            };

        public static string GetServerFor(Language language, ServerType type) => 
            m_languageServerMapping[language].FirstOrDefault(p => p.Type == type)?.Url;
    }
}
