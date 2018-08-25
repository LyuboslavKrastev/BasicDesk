using BasicDesk.App;
using BasicDesk.App.Controllers;
using BasicDesk.App.Models.ViewModels;
using BasicDesk.Data;
using BasicDesk.Models;
using BasicDesk.Tests.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace BasicDesk.Tests.SolutionsControllerTests
{
    [TestClass]
    public class IndexTests
    {
        private BasicDeskDbContext dbContext;

        [TestInitialize]
        public void InitializeTests()
        {
            this.dbContext = TestsDbContext.GetDbContext();
        }

        [TestMethod]
        public async Task Index_ShouldNotAllow_AnnonymousAccess()
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
        public async Task Index__ReturnsAllSolutions()
        {
            var solutions = new[]
            {
                new Solution{Id = 1, Title = "First Solution", Content = "First Content", Author = new User()},
                new Solution{Id = 2, Title = "Second Solution", Content = "Second Content", Author = new User()},
                new Solution{Id = 3, Title = "Third Solution", Content = "Third Content", Author = new User()},

            };

            this.dbContext.Solutions.AddRange(solutions);

            this.dbContext.SaveChanges();

            var controller = new SolutionsController(this.dbContext, TestsAutoMapper.GetMapper());

            var result = await controller.Index() as ViewResult;

            var model = result.Model as ICollection<SolutionListingViewModel>;

            var expectedCollection = solutions.Select(r => r.Id).ToArray();
            var resultCollection = model.Select(rv => rv.Id).ToArray();
            var expectedCount = solutions.Count();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCount, model.Count);
            CollectionAssert.AreEqual(expectedCollection, resultCollection);
        }

    }
}
