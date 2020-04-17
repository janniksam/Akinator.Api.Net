using System.Collections.Generic;
using Akinator.Api.Net.Enumerations;
using Akinator.Api.Net.Model;

namespace Akinator.Api.Net
{
    public class AkinatorServer : IAkinatorServer
    {
        public AkinatorServer(Language language, ServerType serverType, IEnumerable<string> serverUrls)
        {
            ServerUrls = serverUrls;
            Language = language;
            ServerType = serverType;
        }

        public IEnumerable<string> ServerUrls { get; }

        public Language Language { get; }

        public ServerType ServerType { get; }
    }
}