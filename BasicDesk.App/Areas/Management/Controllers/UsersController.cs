using AutoMapper;
using BasicDesk.App.Models.Management.ViewModels;
using BasicDesk.App.Common;
using BasicDesk.App.Helpers.Messages;
using BasicDesk.Common.Constants;
using BasicDesk.Data;
using BasicDesk.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.App.Areas.Management.Controllers
{
    public class UsersController : BaseAdminController
    {
        private readonly BasicDeskDbContext dbContext;
        private readonly UserManager<User> userManager;

        public UsersController(BasicDeskDbContext dbContext, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await this.userManager.GetUserAsync(this.User);

            var dbUsers = GetAllUsersExceptCurrentUser(currentUser);

            var displayUsers = new List<UserConciseViewModel>();

            foreach (var dbUser in dbUsers)
            {
                var roles = await this.userManager.GetRolesAsync(dbUser);
                var user = Mapper.Map<UserConciseViewModel>(dbUser);
                user.IsAdmin = roles.Any(r => r == WebConstants.AdminRole);
                user.IsHelpdeskAgent = roles.Any(r => r == WebConstants.HelpdeskRole);
                user.IsBanned = dbUser.LockoutEnd != null;
                displayUsers.Add(user);
            }

            return View(displayUsers);
        }

        private ICollection<User> GetAllUsersExceptCurrentUser(User currentUser)
        {
            return this.dbContext.Users
                .Where(u => u.Id != currentUser.Id)
                .ToArray();
        }

        [HttpGet]
        public async Task<IActionResult> Ban(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            
            return this.View(Mapper.Map<UserDetailsViewModel>(user));
        }

        [HttpPost]
        [ActionName("Ban")]
        public async Task<IActionResult> Ban(UserDetailsViewModel user)
        {
            var dbUser = await userManager.FindByIdAsync(user.Id);
            await userManager.SetLockoutEndDateAsync(dbUser, DateTime.Today.AddYears(1));

            this.TempData.Put("__Message", new MessageModel()
            {
                Type = MessageType.Success,
                Message = "User banned successfully"
            });


            return this.RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Unban(string id)
        {
            var user = await userManager.FindByIdAsync(id);

            return this.View(Mapper.Map<UserDetailsViewModel>(user));
        }

        public async Task<IActionResult> Unban(UserDetailsViewModel user)
        {
            var dbUser = await this.dbContext.Users.FindAsync(user.Id);

            await userManager.SetLockoutEndDateAsync(dbUser, null);

            this.TempData.Put("__Message", new MessageModel()
            {
                Type = MessageType.Success,
                Message = "User unbanned successfully"
            });

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> PromoteToHelpdesk(string id)
        {
            var user = await this.dbContext.Users.FindAsync(id);

            await userManager.AddToRoleAsync(user, WebConstants.HelpdeskRole);

            return this.RedirectToAction("Index");
        }

        public async Task<IActionResult> DemoteToUser(string id)
        {
            var user = await this.dbContext.Users.FindAsync(id);

            await userManager.RemoveFromRoleAsync(user, WebConstants.HelpdeskRole);

            return this.RedirectToAction("Index");
        }
    }
}