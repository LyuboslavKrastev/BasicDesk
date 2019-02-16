using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicDesk.App.Common;
using BasicDesk.App.Common.Interfaces;
using BasicDesk.App.Helpers.Messages;
using BasicDesk.Common.Constants;
using BasicDesk.Data.Models;
using BasicDesk.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BasicDesk.App.Controllers
{
    public class RepliesController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IRequestService requestService;
        private readonly IAlerter alerter;

        public RepliesController(UserManager<User> userManager, IRequestService requestService, IAlerter alerter)
        {
            this.userManager = userManager;
            this.requestService = requestService;
            this.alerter = alerter;
        }

        [HttpPost]
        public async Task<IActionResult> Create(string requestId, string replyDescription)
        {
            string userId = this.userManager.GetUserId(User);
            bool isTechnician = User.IsInRole(WebConstants.AdminRole) || User.IsInRole(WebConstants.HelpdeskRole);

            await this.requestService.AddReply(int.Parse(requestId), userId, isTechnician, replyDescription);

            this.alerter.AddMessage(MessageType.Success, "Successfully added note");

            return this.RedirectToAction("Details", "Requests", new { id = requestId });
        }
    }
}