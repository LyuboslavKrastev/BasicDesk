using BasicDesk.App.Areas.Management.Controllers;
using BasicDesk.Common.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;


namespace BasicDesk.Tests.UsersControllerTests
{
    [TestClass]
    public class ControllerAccessTests
    {
        [TestMethod]
        public void UsersController_ShouldbeInManagementArea()
        {
            var area = typeof(UsersController)
                .GetCustomAttributes(true)
                .FirstOrDefault(attr => attr is AreaAttribute) as AreaAttribute;

            Assert.IsNotNull(area);
            Assert.AreEqual(WebConstants.ManagementArea, area.RouteValue);
        }

        [TestMethod]
        public void UsersController_ShouldAuthorizeAdministrators()
        {
            var authorization = typeof(UsersController)
                .GetCustomAttributes(true)
                .FirstOrDefault(attr => attr is AuthorizeAttribute) as AuthorizeAttribute;

            Assert.IsNotNull(authorization);
            Assert.AreEqual(WebConstants.AdminRole, authorization.Roles);
        }

        [TestMethod]
        public void UsersController_ShouldNotAuthorizeHelpdeskOrUsers()
        {
            var authorization = typeof(UsersController)
                .GetCustomAttributes(true)
                .FirstOrDefault(attr => attr is AuthorizeAttribute) as AuthorizeAttribute;

            Assert.IsNotNull(authorization);
            Assert.AreNotEqual(WebConstants.HelpdeskRole, authorization.Roles);
            Assert.AreNotEqual(WebConstants.UserRole, authorization.Roles);
        }
    }

}
