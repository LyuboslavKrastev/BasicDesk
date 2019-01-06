using BasicDesk.Data.Models.Solution;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace BasicDesk.Tests.Utilities
{
    public static class TestsUserManager
    {
        public  static Mock<UserManager<User>> GetUserManager()
        {

            var mockUserStore = new Mock<IUserStore<User>>();

            var mockUserManager = new Mock<UserManager<User>>
                (mockUserStore.Object, null, null, null, null, null, null, null, null);

            return mockUserManager;

        }
    }
}
