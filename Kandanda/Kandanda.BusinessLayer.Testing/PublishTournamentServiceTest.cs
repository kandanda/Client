using System;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using Kandanda.BusinessLayer.ServiceImplementations;
using Kandanda.BusinessLayer.ServiceInterfaces;
using Kandanda.Dal.DataTransferObjects;
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
            var fakeHandler = new FakeResponseHandler
            {
                Response =
                    () =>
                        new HttpResponseMessage(HttpStatusCode.Accepted)
                        {
                            Content =
                                new StringContent(
                                    "{ \"tournament\":{ \"id\":5,\"link\":\"/tournaments/b3deda3f0e79f609cdb4608377079915\"} }",
                                    Encoding.UTF8, "application/json")
                        }
            };
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
        public async Task PostTournamentAsyncShouldReturnCorrectResponse()
        {
            var fakeHandler = new FakeResponseHandler
            {
                Response =
                    () =>
                        new HttpResponseMessage(HttpStatusCode.Accepted)
                        {
                            Content =
                                new StringContent(
                                    "{ \"tournament\":{ \"id\":5,\"link\":\"/tournaments/acbd\"} }",
                                    Encoding.UTF8, "application/json")
                        }
            };
            var service = new PublishTournamentService(new Uri("https://someurl.com/"), CreateFakeTournamentBuilder(), fakeHandler);

            var response = await service.PostTournamentAsync(new Tournament(), "token", CancellationToken.None);
            Assert.AreEqual(response.Id, 5);
            Assert.AreEqual(response.Link, new Uri("https://someurl.com/tournaments/acbd"));
        }

        [TestMethod]
        [ExpectedException(typeof(AuthenticationException))]
        public async Task PostTournamentAsyncShouldThrowIfAuthTokenExpired()
        {
            var fakeHandler = new FakeResponseHandler { Response = () => new HttpResponseMessage(HttpStatusCode.Redirect) };
            var service = new PublishTournamentService(new Uri("https://someurl.com/"), CreateFakeTournamentBuilder(), fakeHandler);

            await service.PostTournamentAsync(new Tournament(), "token", CancellationToken.None);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task PostTournamenAsyncShouldThrowIfAuthTokenIsEmpty()
        {
            var fakeHandler = new FakeResponseHandler();
            var service = new PublishTournamentService(new Uri("https://someurl.com/"), CreateFakeTournamentBuilder(), fakeHandler);

            await service.PostTournamentAsync(new Tournament(), "", CancellationToken.None);
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
