using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Akinator.Api.Net.Enumerations;
using Akinator.Api.Net.Model;

namespace Akinator.Api.Net
{
    public interface IAkinatorServerLocator
    {
        Task<IAkinatorServer> SearchAsync(Language language, ServerType serverType, CancellationToken cancellationToken = default);
        
        Task<IEnumerable<IAkinatorServer>> SearchAllAsync(Language Language, CancellationToken cancellationToken = default);
    }
}
