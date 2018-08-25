using BasicDesk.Data;
using BasicDesk.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using BasicDesk.Common.Constants;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace BasicDesk.App.Common
{
    public static class ApplicationBuilderAuthExtensions
    {
        private const string DefaultAdminPassword = "admin123";
        private const string DefaultHelpdeskPassword = "help123";

        private static IdentityRole[] roles =
        {
            new IdentityRole(WebConstants.AdminRole),
            new IdentityRole(WebConstants.HelpdeskRole),
            new IdentityRole(WebConstants.UserRole)
        };

        public static async void SeedDatabase(this IApplicationBuilder applicationBuilder)
        {
            var serviceFactory = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            var scope = serviceFactory.CreateScope();

            using (scope)
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var dbContext = scope.ServiceProvider.GetRequiredService<BasicDeskDbContext>();

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Name))
                    {
                        await roleManager.CreateAsync(role);
                    }
                }

                var adminUser = await userManager.FindByNameAsync("admin");
                var hdUser = await userManager.FindByNameAsync("hduser");

                if (adminUser == null)
                {
                    adminUser = new User
                    {
                        UserName = "admin",
                        Email = "admin@nevermind.com",
                        FullName = "Default Admin"
                    };

                    await userManager.CreateAsync(adminUser, DefaultAdminPassword);
                    await userManager.AddToRoleAsync(adminUser, roles[0].Name);
                }

                if (hdUser == null)
                {
                    hdUser = new User
                    {
                        UserName = "hduser",
                        Email = "hduser@nevermind.com",
                        FullName = "Default Helpdesk"
                    };

                    await userManager.CreateAsync(hdUser, DefaultHelpdeskPassword);
                    await userManager.AddToRoleAsync(hdUser, roles[1].Name);
                }

                if (!dbContext.RequestStatuses.Any())
                {
                    string[] statuses = new string[]
                    {
                        "For Approval",
                        "Hardware Replacement",
                        "In Process",
                        "On Hold",
                        "Open",
                        "Rejected",
                        "Closed"
                    };

                    foreach (var status in statuses)
                    {
                        dbContext.RequestStatuses.Add(new RequestStatus { Name = status });
                    }

                    dbContext.SaveChanges();
                }
            }
        }
    }
}
