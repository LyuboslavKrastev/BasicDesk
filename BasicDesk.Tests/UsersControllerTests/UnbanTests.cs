using BasicDesk.App.Areas.Management.Controllers;
using BasicDesk.App.Areas.Management.Models.ViewModels;
using BasicDesk.Data.Models.Solution;
using BasicDesk.Tests.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace BasicDesk.Tests.UsersControllerTests
{
    [TestClass]
    public class UnbanTests
    {
        [TestMethod]
        public async Task Unban_ReturnsCorrectViewModel()
        {
            var users = new[]
            {
                new User{Id = "123"},
                new User{Id  = "321"},
                new User{Id = "456" }
            };

            var context = TestsDbContext.GetDbContext();
            context.Users.AddRange(users);

            context.SaveChanges();

            var mockUserManager = TestsUserManager.GetUserManager();

            mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>()))
             .ReturnsAsync(users[0]);

            var controller = new UsersController(context, TestsAutoMapper.GetMapper(), mockUserManager.Object);

            var result = await controller.Unban(users[0].Id);

            var model = (result as ViewResult).ViewData.Model as UserDetailsViewModel;

            Assert.AreEqual(model.Id, users[0].Id);
            Assert.IsInstanceOfType(model, typeof(UserDetailsViewModel));
        }

        [TestMethod]
        public async Task Unban_ReturnsCorrectView()
        {
            var mockUserManager = TestsUserManager.GetUserManager();
            var user = new User { Id = "1" };

            var context = TestsDbContext.GetDbContext();
            context.Users.Add(user);

            mockUserManager.Setup(um => um.FindByIdAsync(It.IsAny<string>()))
             .ReturnsAsync(user);

            var controller = new UsersController(context, TestsAutoMapper.GetMapper(), TestsUserManager.GetUserManager().Object);

            var result = await controller.Unban(user.Id);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
