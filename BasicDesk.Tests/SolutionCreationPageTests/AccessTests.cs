using BasicDesk.Common.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using BasicDesk.App.Areas.Management.Pages.Solutions;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using BasicDesk.App;

namespace BasicDesk.Tests.SolutionCreationPageTests
{
    [TestClass]
    public class AccessTests
    {
        [TestMethod]
        public async Task SolutionCreationPage_ShouldNotAllow_AnnonymousAccess()
        {
            var factory = new WebApplicationFactory<Startup>();
            var client = factory.CreateClient();

            var result = await client.GetAsync("/solutions");
            var resultContent = await result.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsTrue(resultContent.Contains("Forgot your password?"));
            Assert.IsTrue(resultContent.Contains("Register as a new user"));
        }

        [TestMethod]
        public void SolutionCreationPage_ShouldbeInManagementArea()
        {
            var area = typeof(CreateModel)
                .GetCustomAttributes(true)
                .FirstOrDefault(attr => attr is AreaAttribute) as AreaAttribute;

            Assert.IsNotNull(area);
            Assert.AreEqual(WebConstants.ManagementArea, area.RouteValue);
        }

        [TestMethod]
        public void SolutionCreationPage_ShouldAuthorizeAdministratorsAndHelpdeskAgents()
        {
            var authorization = typeof(CreateModel)
                .GetCustomAttributes(true)
                .FirstOrDefault(attr => attr is AuthorizeAttribute) as AuthorizeAttribute;

            Assert.IsNotNull(authorization);
            Assert.AreEqual(WebConstants.ManagementRoles, authorization.Roles);
        }

        [TestMethod]
        public void SolutionCreationPage_ShouldNotAuthorizeUsers()
        {
            var authorization = typeof(CreateModel)
                .GetCustomAttributes(true)
                .FirstOrDefault(attr => attr is AuthorizeAttribute) as AuthorizeAttribute;

            Assert.IsNotNull(authorization);

            Assert.IsFalse(authorization.Roles.Contains(WebConstants.UserRole));
        }    
    }
}
