using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kandanda.BusinessLayer
{
    public interface IPublishTournamentService : IDisposable
    {
        Task<string> AuthenticateAsync(string username, string password, CancellationToken cancellationToken);
        Task<string> PostTournamentAsync(string payload, string authToken, CancellationToken cancellationToken);
    }
}
