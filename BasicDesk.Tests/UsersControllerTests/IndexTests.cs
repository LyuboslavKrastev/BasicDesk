using BasicDesk.App.Areas.Management.Controllers;
using BasicDesk.App.Areas.Management.Models.ViewModels;
using BasicDesk.Models;
using BasicDesk.Tests.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.Tests.UsersControllerTests
{
    [TestClass]
    public class IndexTests
    {
        [TestMethod]
        public async Task Index_ShouldReturnAllUsers_ExceptCurrent()
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

            mockUserManager.Setup(um => um.GetUserAsync(null))
                .ReturnsAsync(users[0]);

            mockUserManager.Setup(um => um.GetRolesAsync(It.IsAny<User>()))
                .ReturnsAsync(new List<string> { "Whatever" });

            var controller = new UsersController(context, TestsAutoMapper.GetMapper(), mockUserManager.Object);

            var result = await controller.Index() as ViewResult;
            var model = result.Model as IEnumerable<UserConciseViewModel>;
            var expectedCollection = users.Where(u => u.Id != users[0].Id)
                .Select(u => u.Id).ToArray();
            var resultCollection = model.Select(u => u.Id).ToArray();

            Assert.IsNotNull(result);
            CollectionAssert.AreEqual(expectedCollection, resultCollection);
        }
    }
}
