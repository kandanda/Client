using System;
using System.Security;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Kandanda.Dal.DataTransferObjects;

namespace Kandanda.BusinessLayer.ServiceInterfaces
{
    public interface IPublishTournamentService : IDisposable
    {
        /// <summary>
        /// Get a auth token from API. 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns JWT Token or null if failed</returns>
        Task<string> AuthenticateAsync(string email, SecureString password, CancellationToken cancellationToken);

        /// <summary>
        /// Post a new tournament plan online
        /// </summary>
        /// <param name="tournament"></param>
        /// <param name="authToken"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentException">Throws if auth token is null or empty</exception>
        /// <exception cref="AuthenticationException">Throws if the auth token is exipired</exception>
        /// <returns></returns>
        Task<string> PostTournamentAsync(Tournament tournament, string authToken, CancellationToken cancellationToken);

        Uri BaseUri { get; }
    }
}
