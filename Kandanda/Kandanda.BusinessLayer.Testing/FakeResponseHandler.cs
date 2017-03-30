using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kandanda.BusinessLayer.Testing
{
    public class FakeResponseHandler : DelegatingHandler
    {
        public List<Tuple<HttpRequestMessage, string>> Requests { get; } = new List<Tuple<HttpRequestMessage, string>>();

        public Func<HttpResponseMessage> Response { get; set; } = () => new HttpResponseMessage(HttpStatusCode.Accepted)
        {
            Content = new StringContent("", Encoding.UTF8, "application/json")
        };

        protected override async Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken)
        {
            Requests.Add(new Tuple<HttpRequestMessage, string>(request, await request.Content.ReadAsStringAsync()));
            return Response();
        }
    }
}
