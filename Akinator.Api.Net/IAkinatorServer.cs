using System.Collections.Generic;
using Akinator.Api.Net.Enumerations;
using Akinator.Api.Net.Model;

namespace Akinator.Api.Net
{
    public interface IAkinatorServer
    {
        Language Language { get; }

        ServerType ServerType { get; }

        string BaseId { get; }

        string ServerUrl { get; }
    }
}