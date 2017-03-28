using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Kandanda.BusinessLayer.Testing
{
    public class FakeResponseHandler : DelegatingHandler
    {
        private readonly Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> _sendCallback;
        public FakeResponseHandler(Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> sendCallback)
        {
            _sendCallback = sendCallback;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
                HttpRequestMessage request,
                CancellationToken cancellationToken)
        {
            return await _sendCallback(request, cancellationToken);
        }
    }
}
