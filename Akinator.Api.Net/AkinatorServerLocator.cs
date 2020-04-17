using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Akinator.Api.Net.Enumerations;
using Akinator.Api.Net.Model;
using Akinator.Api.Net.Model.External;
using Akinator.Api.Net.Utils;

namespace Akinator.Api.Net
{
    public class AkinatorServerLocator : IAkinatorServerLocator
    {
        private const string ServerListUrl =
            "https://global3.akinator.com/ws/instances_v2.php?media_id=14&footprint=cd8e6509f3420878e18d75b9831b317f&mode=https";

        private readonly AkiWebClient m_webClient;
        private List<IAkinatorServer> m_cachedServers;

        public AkinatorServerLocator()
        {
            m_webClient = new AkiWebClient();
        }

        public async Task<IAkinatorServer> SearchAsync(Language language, ServerType serverType,
            CancellationToken cancellationToken = default)
        {
            await EnsureServersAsync(cancellationToken).ConfigureAwait(false);

            return m_cachedServers.FirstOrDefault(p =>
                p.ServerType == serverType &&
                p.Language == language);
        }

        public async Task<IEnumerable<IAkinatorServer>> SearchAllAsync(Language language, CancellationToken cancellationToken = default)
        {
            await EnsureServersAsync(cancellationToken).ConfigureAwait(false);
            
            return m_cachedServers.Where(p =>
                p.Language == language);
        }

        private async Task EnsureServersAsync(CancellationToken cancellationToken)
        {
            if (m_cachedServers == null)
            {
                m_cachedServers = await LoadServersAsync(cancellationToken).ConfigureAwait(false);
            }
        }

        private async Task<List<IAkinatorServer>> LoadServersAsync(CancellationToken cancellationToken)
        {
            var response = await m_webClient.GetAsync(ServerListUrl, cancellationToken).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var serverListRaw = XmlConverter.ToClass<ServerSearchResult>(content);
            return MapToServerList(serverListRaw);
        }

        private static List<IAkinatorServer> MapToServerList(ServerSearchResult serverListRaw)
        {
            var instancesRaw = serverListRaw?.PARAMETERS?.INSTANCE;
            if (instancesRaw == null)
            {
                return new List<IAkinatorServer>();
            }

            var servers = new List<IAkinatorServer>();
            foreach (var instanceRaw in instancesRaw)
            {
                var language = MapLanguage(instanceRaw.LANGUAGE?.LANG_ID);
                var serverType = MapServerType(instanceRaw.SUBJECT?.SUBJ_ID);
                var serverUrls = instanceRaw.CANDIDATS?.URL?.ToArray() ?? new string[0];
                if (serverUrls.Length == 0)
                {
                    continue;
                }

                servers.Add(new AkinatorServer(language, serverType, serverUrls));
            }

            return servers;
        }

        private static ServerType MapServerType(string serverTypeCode)
        {
            switch (serverTypeCode)
            {
                case "1":
                {
                    return ServerType.Person;
                }
                case "2":
                {
                    return ServerType.Object;
                }
                case "7":
                {
                    return ServerType.Places;
                }
                case "13":
                {
                    return ServerType.Movie;
                }
                case "14":
                {
                    return ServerType.Animal;
                }
                default:
                {
                    throw new NotSupportedException(
                        $"Server-Type with the {serverTypeCode} is currently not supported.");
                }
            }
        }

        private static Language MapLanguage(string languageCode)
        {
            switch (languageCode)
            {
                case "ar":
                {
                    return Language.Arabic;
                }
                case "cn":
                {
                    return Language.Chinese;
                }
                case "en":
                {
                    return Language.English;
                }
                case "fr":
                {
                    return Language.French;
                }
                case "de":
                {
                    return Language.German;
                }
                case "id":
                {
                    return Language.Indonesian;
                }
                case "il":
                {
                    return Language.Israel;
                }
                case "it":
                {
                    return Language.Italian;
                }
                case "jp":
                {
                    return Language.Japan;
                }
                case "kr":
                {
                    return Language.Korean;
                }
                case "nl":
                {
                    return Language.Netherlands;
                }
                case "pl":
                {
                    return Language.Polski;
                }
                case "pt":
                {
                    return Language.Portuguese;
                }
                case "es":
                {
                    return Language.Spanish;
                }
                case "ru":
                {
                    return Language.Russian;
                }
                case "tr":
                {
                    return Language.Turkish;
                }
                default:
                {
                    throw new NotSupportedException(
                        $"Language with the {languageCode} is currently not supported.");
                }
            }
        }
    }
}