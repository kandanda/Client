using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal.DataTransferObjects;
using Newtonsoft.Json.Linq;

namespace Kandanda.BusinessLayer.ServiceImplementations
{
    public class PublishTournamentService : IPublishTournamentService
    {
        private readonly HttpClient _client;
        private readonly IPublishTournamentRequestBuilder _publishTournamentRequestBuilder;
        public string ApiVersion { get; } = "v1";

        public Uri BaseUri { get; }

        [ExcludeFromCodeCoverage]
        public PublishTournamentService(Uri baseUri, IPublishTournamentRequestBuilder publishTournamentRequestBuilder)
            : this(baseUri, publishTournamentRequestBuilder, null)
        {}

        public PublishTournamentService(Uri baseUri, IPublishTournamentRequestBuilder publishTournamentRequestBuilder, 
            HttpMessageHandler handler)
        {
            _publishTournamentRequestBuilder = publishTournamentRequestBuilder;
            _client = handler == null ? new HttpClient() : new HttpClient(handler);
            _client.BaseAddress = new Uri(baseUri, $"/api/{ApiVersion}/");
            BaseUri = baseUri;
        }

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

        public async Task<PublishTournamentResponse> PostTournamentAsync(Tournament tournament, string authToken, CancellationToken cancellationToken)
        {
            var payload = await PostTournamentAsync(_publishTournamentRequestBuilder.BuildJsonRequest(tournament), authToken, cancellationToken);
            return PublishTournamentResponse.Create(payload, BaseUri);
        }

        private async Task<string> PostTournamentAsync(string payload, string authToken, CancellationToken cancellationToken)
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
