using System.Collections.Generic;
using Akinator.Api.Net.Enumerations;
using Akinator.Api.Net.Model;

namespace Akinator.Api.Net
{
    public class AkinatorServer : IAkinatorServer
    {
        public AkinatorServer(Language language, ServerType serverType, string baseId, string serverUrls)
        {
            ServerUrl = serverUrls;
            Language = language;
            ServerType = serverType;
            BaseId = baseId;
        }

        public Language Language { get; }

        public ServerType ServerType { get; }

        public string BaseId { get; }

        public string ServerUrl { get; }
    }
}