using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Kandanda.BusinessLayer.Net
{
    public class PublishTournamentService : IPublishTournamentService
    {
        private readonly HttpClient _client;
        public string ApiVersion { get; } = "v1";

        public PublishTournamentService(Uri baseUri, HttpMessageHandler handler = null)
        {
            _client = handler == null ? new HttpClient() : new HttpClient(handler);
            _client.BaseAddress = new Uri(baseUri, $"/api/{ApiVersion}/");
        }

        /// <summary>
        /// Get a auth token from API. 
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Returns JWT Token or null if failed</returns>
        public async Task<string> AuthenticateAsync(string email, string password, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "auth")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["email"] = email,
                    ["password"] = password
                })
            };

            using (var response = await _client.SendAsync(request, cancellationToken))
            using (var content = response.Content)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized)
                    return null;
                response.EnsureSuccessStatusCode();
                return JObject.Parse(await content.ReadAsStringAsync()).SelectToken("auth_token").ToString();
            }
        }

        /// <summary>
        /// Post a new tournament plan online
        /// </summary>
        /// <param name="payload"></param>
        /// <param name="authToken"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="ArgumentException">Throws if auth token is null or empty</exception>
        /// <exception cref="AuthenticationException">Throws if the auth token is exipired</exception>
        /// <returns></returns>
        public async Task<string> PostTournamentAsync(string payload, string authToken, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(authToken))
                throw new ArgumentException("Invalid AuthToken");

            var request = new HttpRequestMessage(HttpMethod.Post, "tournaments")
            {
                Content = new StringContent(payload, Encoding.UTF8, "application/json")
            };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            using (var response = await _client.SendAsync(request, cancellationToken))
            {
                // Api redirects to sign in page if the auth token is expired
                if (response.StatusCode == HttpStatusCode.Redirect)
                {
                    throw new AuthenticationException("JWT Token expired");
                }
                using (var content = response.Content)
                {
                    return await content.ReadAsStringAsync();
                }
            }
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
