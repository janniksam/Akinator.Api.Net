using Akinator.Api.Net.Enumerations;

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