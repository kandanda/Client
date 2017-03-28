using System;
using System.Text;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Kandanda.BusinessLayer.Testing
{
    /// <summary>
    /// Summary description for PublishTournamentServiceTest
    /// </summary>
    [TestClass]
    public class PublishTournamentServiceTest
    {
        public PublishTournamentServiceTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public async Task AuthenticateAsyncShouldReturnNullWhenCrendialsAreWrong()
        {
        }

        [TestMethod]
        public async Task AuthenticateAsyncShouldAccessCorrectUri()
        {
            var baseUrl = new Uri("https://someurl.com/");
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
            var fakeHandler = new FakeResponseHandler(async (req, canc) =>
            {
                Assert.AreEqual(baseUrl + "api/v1/auth", req.RequestUri.ToString());
                return CreateSuccessfulFakeResponseMessage();
            });
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
            var service = new PublishTournamentService(baseUrl, fakeHandler);
            await service.AuthenticateAsync("abc@def.com", "password", CancellationToken.None);
        }

        [TestMethod]
        public async Task AutheticateAsyncShouldSendCorrectContent()
        {
            var fakeHandler = new FakeResponseHandler(async (req, canc) =>
            {
                Assert.AreEqual("email=abc%40def.com&password=password", await req.Content.ReadAsStringAsync());
                return CreateSuccessfulFakeResponseMessage();
            });
            var service = new PublishTournamentService(new Uri("https://someurl.com/"), fakeHandler);
            await service.AuthenticateAsync("abc@def.com", "password", CancellationToken.None);
        }

        private HttpResponseMessage CreateSuccessfulFakeResponseMessage(string authToken="xxxx.xxxx.xxxx")
        {
            return new HttpResponseMessage(HttpStatusCode.Accepted)
            {
                Content = new StringContent("{ \"auth_token\": \"" + authToken + "\" }", Encoding.UTF8, "application/json")
            };
        }
    }
}
