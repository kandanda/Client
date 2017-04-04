using System;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Kandanda.BusinessLayer.Testing
{
    [TestClass]
    public class PublishTournamentServiceTest
    {
        [TestMethod]
        public async Task AuthenticateAsyncShouldReturnNullWhenCrendialsAreWrong()
        {
            var fakeHandler = new FakeResponseHandler { Response = () =>  new HttpResponseMessage(HttpStatusCode.Unauthorized)};
            var service = new PublishTournamentService(new Uri("https://someurl.com/"), CreateFakeTournamentBuilder(), fakeHandler);

            var actual = await service.AuthenticateAsync("abc@def.com", "password", CancellationToken.None);
            Assert.IsNull(actual);
        }

        [TestMethod]
        public async Task AuthenticateAsyncShouldSendACorrectRequest()
        {
            var fakeHandler = new FakeResponseHandler {Response = () => CreateSuccessfulFakeAuthResponse()};
            var service = new PublishTournamentService(new Uri("https://someurl.com/"), CreateFakeTournamentBuilder(), fakeHandler);

            await service.AuthenticateAsync("abc@def.com", "password", CancellationToken.None);

            var request = fakeHandler.Requests[0].Item1;
            var content = fakeHandler.Requests[0].Item2;
            Assert.AreEqual(HttpMethod.Post, request.Method);
            Assert.AreEqual(new Uri("https://someurl.com/api/v1/auth"), request.RequestUri);
            Assert.AreEqual("email=abc%40def.com&password=password", content);
        }

        [TestMethod]
        public async Task AutheticateAsyncShouldReturnAuthToken()
        {
            const string authToken = "xxxx.xxxx.xxxx";
            var fakeHandler = new FakeResponseHandler { Response = () => CreateSuccessfulFakeAuthResponse(authToken) };
            var service = new PublishTournamentService(new Uri("https://someurl.com/"), CreateFakeTournamentBuilder(), fakeHandler);

            var actual = await service.AuthenticateAsync("abc@def.com", "password", CancellationToken.None);
            Assert.AreEqual(authToken, actual);
        }


        [TestMethod]
        public async Task PostTournamentAsyncShouldSendACorrectRequest()
        {
            var fakeHandler = new FakeResponseHandler();
            var service = new PublishTournamentService(new Uri("https://someurl.com/"), CreateFakeTournamentBuilder(), fakeHandler);

            await service.PostTournamentAsync(new Tournament(), "token", CancellationToken.None);

            var request = fakeHandler.Requests[0].Item1;
            var content = fakeHandler.Requests[0].Item2;
            Assert.AreEqual(HttpMethod.Post, request.Method);
            Assert.AreEqual(new Uri("https://someurl.com/api/v1/tournaments"), request.RequestUri);
            Assert.AreEqual("Bearer token", request.Headers.Authorization.ToString());
            Assert.AreEqual("payload", content);
        }

        [TestMethod]
        [ExpectedException(typeof(AuthenticationException))]
        public async Task PostTournamentAsyncShouldThrowIfAuthTokenExpired()
        {
            var fakeHandler = new FakeResponseHandler { Response = () => new HttpResponseMessage(HttpStatusCode.Redirect) };
            var service = new PublishTournamentService(new Uri("https://someurl.com/"), CreateFakeTournamentBuilder(), fakeHandler);

            await service.PostTournamentAsync(new Tournament(), "token", CancellationToken.None);
        }

        private IPublishTournamentRequestBuilder CreateFakeTournamentBuilder()
        {
            var builder = new Mock<IPublishTournamentRequestBuilder>();
            builder.Setup(t => t.BuildJsonRequest(It.IsAny<Tournament>())).Returns("payload");
            return builder.Object;
        }

        private HttpResponseMessage CreateSuccessfulFakeAuthResponse(string authToken="xxxx.xxxx.xxxx")
        {
            return new HttpResponseMessage(HttpStatusCode.Accepted)
            {
                Content = new StringContent("{ \"auth_token\": \"" + authToken + "\" }", Encoding.UTF8, "application/json")
            };
        }
    }
}
