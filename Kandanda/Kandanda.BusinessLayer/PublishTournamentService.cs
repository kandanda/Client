using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Kandanda.BusinessLayer
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

        public async Task<string> AuthenticateAsync(string username, string password, CancellationToken cancellationToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "auth")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    ["email"] = username,
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
            using (var content = response.Content)
            {
                return await content.ReadAsStringAsync();
            }
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}
