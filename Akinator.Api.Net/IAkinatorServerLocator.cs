using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Akinator.Api.Net.Enumerations;
using Akinator.Api.Net.Model;
using Akinator.Api.Net.Model.External;
using Akinator.Api.Net.Utils;

namespace Akinator.Api.Net
{
    public interface IAkinatorServerLocator
    {
        Task<IAkinatorServer> Search(Language language, ServerType serverType, CancellationToken cancellationToken = default);
    }

    public class AkinatorServerLocator : IAkinatorServerLocator
    {
        private const string ServerListUrl =
            "https://global3.akinator.com/ws/instances_v2.php?media_id=14&footprint=cd8e6509f3420878e18d75b9831b317f&mode=https";

        private readonly AkiWebClient m_webClient;

        public AkinatorServerLocator()
        {
            m_webClient = new AkiWebClient();
        }

        public async Task<IAkinatorServer> Search(Language language, ServerType serverType, CancellationToken cancellationToken = default)
        {
            var response = await m_webClient.GetAsync(ServerListUrl, cancellationToken).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var result = XmlConverter.ToClass<ServerSearchResult>(content);
            return null;
        }
    }

    public interface IAkinatorServer
    {
        Language Language { get; }

        ServerType ServerType { get; }
    }

    public class AkinatorServer : IAkinatorServer
    {
        public AkinatorServer(Language language, ServerType serverType)
        {
            Language = language;
            ServerType = serverType;
        }

        public Language Language { get; }

        public ServerType ServerType { get; }
    }
}
