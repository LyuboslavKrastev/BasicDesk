using BasicDesk.App;
using BasicDesk.Common.Constants;
using BasicDesk.App.Controllers;
using BasicDesk.App.Models.ViewModels;
using BasicDesk.Data;
using BasicDesk.Models;
using BasicDesk.Tests.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BasicDesk.Tests.RequestsControllerTests
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

            var result = await client.GetAsync("/requests");
            var resultContent = await result.Content.ReadAsStringAsync();

            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.IsTrue(resultContent.Contains("Forgot your password?"));
            Assert.IsTrue(resultContent.Contains("Register as a new user"));
        }

        [TestMethod]
        public void Index_WithoutFilter_ReturnsAllRequests()
        {
            var requests = new[]
            {
                new Request
                {
                    Id = 1,
                    AssignedTo = new User(),
                    Requester = new User(),
                    Status = new RequestStatus()
                },
                 new Request
                {
                    Id = 2,
                    AssignedTo = new User(),
                    Requester = new User(),
                    Status = new RequestStatus()
                },      new Request
                {
                    Id = 3,
                    AssignedTo = new User(),
                    Requester = new User(),
                    Status = new RequestStatus()
                },
            };

            this.dbContext.Requests.AddRange(requests);

            this.dbContext.SaveChanges();

            var mockUserManager = TestsUserManager.GetUserManager();

            var controller = new RequestsController(this.dbContext, TestsAutoMapper.GetMapper(), mockUserManager.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
              {
                        new Claim(ClaimTypes.Role, WebConstants.AdminRole)
                    }))
                }
            };

            var result = controller.Index("", "", "", null) as ViewResult;

            var model = result.Model as RequestSortingViewModel;

            //the default sorting order is Id => desc
            var expectedCollection = requests.Select(r => r.Id).OrderByDescending(r => r).ToArray();
            var resultCollection = model.RequestViews.Select(rv => rv.Id).ToArray();

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expectedCollection, resultCollection);
        }

        [TestMethod]
        public void Index_WithOpenFilter_ReturnsAllNonClosedRequests_ForCurrentUser()
        {
            var openStatus = new RequestStatus { Id = 1, Name = "Open" };

            var closedStatus = new RequestStatus { Id = 2, Name = "Closed" };

            var users = new[]
            {
                new User{Id = "123"},
                new User{Id  = "321"},
                new User{Id = "456" }
            };

            var requests = new[]
            {
                new Request{ Id = 1, AssignedTo = new User(), Requester = users[0], Status = openStatus},
                new Request{ Id = 2, AssignedTo = new User(),Requester = users[1], Status = openStatus},
                new Request{ Id = 3, AssignedTo = new User(),Requester = users[2], Status = closedStatus},
                new Request{ Id = 4, AssignedTo = new User(), Requester = users[0], Status = closedStatus},
                new Request{ Id = 5, AssignedTo = new User(), Requester = users[0], Status = closedStatus},

            };

            this.dbContext.Users.AddRange(users);

            this.dbContext.Requests.AddRange(requests);

            this.dbContext.SaveChanges();

            var mockUserManager = TestsUserManager.GetUserManager();

            mockUserManager.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns("123");

            var controller = new RequestsController(this.dbContext, TestsAutoMapper.GetMapper(), mockUserManager.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
              {
                        new Claim(ClaimTypes.Role, WebConstants.AdminRole)
                    }))
                }
            };

            var result = controller.Index("", "", "MyOpen", null) as ViewResult;

            var model = result.Model as RequestSortingViewModel;

            //the default sorting order is Id => desc
            var expectedCount = 1;
            var expectedCollection = requests
                .Where(r => r.Requester == users[0] && r.Status == openStatus)
                .Select(r => r.Id).OrderByDescending(r => r).ToArray();
            var resultCollection = model.RequestViews.Select(rv => rv.Id).ToArray();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCount, resultCollection.Count());
            CollectionAssert.AreEqual(expectedCollection, resultCollection);
        }

        [TestMethod]
        public void Index_WithOpenFilter_ReturnsAllClosedRequests_ForCurrentUser()
        {
            var openStatus = new RequestStatus { Id = 1, Name = "Open" };

            var closedStatus = new RequestStatus { Id = 2, Name = "Closed" };

            var users = new[]
            {
                new User{Id = "123"},
                new User{Id  = "321"},
                new User{Id = "456" }
            };

            var requests = new[]
            {
                new Request{ Id = 1, AssignedTo = new User(), Requester = users[0], Status = openStatus},
                new Request{ Id = 2, AssignedTo = new User(),Requester = users[1], Status = openStatus},
                new Request{ Id = 3, AssignedTo = new User(),Requester = users[2], Status = closedStatus},
                new Request{ Id = 4, AssignedTo = new User(), Requester = users[0], Status = closedStatus},
                new Request{ Id = 5, AssignedTo = new User(), Requester = users[0], Status = closedStatus},

            };

            this.dbContext.Users.AddRange(users);

            this.dbContext.Requests.AddRange(requests);

            this.dbContext.SaveChanges();

            var mockUserManager = TestsUserManager.GetUserManager();

            mockUserManager.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>()))
                .Returns("123");

            var controller = new RequestsController(this.dbContext, TestsAutoMapper.GetMapper(), mockUserManager.Object);

            controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(new ClaimsIdentity(new[]
              {
                        new Claim(ClaimTypes.Role, WebConstants.AdminRole)
                    }))
                }
            };

            var result = controller.Index("", "", "MyClosed", null) as ViewResult;

            var model = result.Model as RequestSortingViewModel;

            //the default sorting order is Id => desc
            var expectedCount = 2;
            var expectedCollection = requests
                .Where(r => r.Requester == users[0] && r.Status == closedStatus)
                .Select(r => r.Id).OrderByDescending(r => r).ToArray();
            var resultCollection = model.RequestViews.Select(rv => rv.Id).ToArray();

            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCount, resultCollection.Count());
            CollectionAssert.AreEqual(expectedCollection, resultCollection);
        }

    }
}
