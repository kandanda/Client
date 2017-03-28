using System;
using System.Threading;
using System.Threading.Tasks;

namespace Kandanda.BusinessLayer.ServiceInterfaces
{
    public interface IPublishTournamentService : IDisposable
    {
        Task<string> AuthenticateAsync(string email, string password, CancellationToken cancellationToken);
        Task<string> PostTournamentAsync(string payload, string authToken, CancellationToken cancellationToken);
    }
}
