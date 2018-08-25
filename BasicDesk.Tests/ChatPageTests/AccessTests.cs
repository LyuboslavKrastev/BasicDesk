using BasicDesk.App;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading.Tasks;

namespace BasicDesk.Tests.ChatPageTests
{
    [TestClass]
    public class AccessTests
    {
        [TestMethod]
        public async Task ChatPage_ShouldNotAllow_AnnonymousAccess()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();

            var result = await client.GetAsync("/chat");
            var resultContent = await result.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsTrue(resultContent.Contains("Forgot your password?"));
            Assert.IsTrue(resultContent.Contains("Register as a new user"));
        }
    }
}
