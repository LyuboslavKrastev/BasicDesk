using BasicDesk.Data;
using BasicDesk.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BasicDesk.App.Areas.Management.Pages.Solutions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using BasicDesk.Common.Constants;
using BasicDesk.App.Areas.Management.Models.BindingModels;
using System.Linq;

namespace BasicDesk.Tests.SolutionCreationPageTests
{
    [TestClass]
    public class CreateTests
    {
        private BasicDeskDbContext dbContext;

        [TestInitialize]
        public void InitializeTests()
        {
            this.dbContext = TestsDbContext.GetDbContext();
        }

        [TestMethod]
        public void OnPost_AddsSolutionToTheDatabase()
        {
            var mockUserManager = TestsUserManager.GetUserManager();

            var page = new CreateModel(this.dbContext, mockUserManager.Object, TestsAutoMapper.GetMapper());

            page.PageContext = new PageContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
              {
                        new Claim(ClaimTypes.Role, WebConstants.AdminRole)
                    }))
                }
            };

            page.Model = new SolutionCreationBindingModel
            {
                Title = "Title",
                Content = "Content"
            };

            var result = page.OnPost();

            var lastRequest = this.dbContext.Solutions.LastOrDefault();

            Assert.IsNotNull(lastRequest);
            Assert.IsTrue(lastRequest.Content == page.Model.Content && lastRequest.Content == page.Model.Content);
        }
    }
}
