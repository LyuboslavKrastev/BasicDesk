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
    public class NotesController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IRequestService requestService;
        private readonly IAlerter alerter;

        public NotesController(UserManager<User> userManager, IRequestService requestService, IAlerter alerter)
        {
            this.userManager = userManager;
            this.requestService = requestService;
            this.alerter = alerter;
        }

        [HttpPost]
        public async Task<IActionResult> AddNote(string requestId, string noteDescription)
        {
            string userId = this.userManager.GetUserId(User);
            string userName = this.User.Identity.Name;
            bool isTechnician = User.IsInRole(WebConstants.AdminRole) || User.IsInRole(WebConstants.HelpdeskRole);

            await this.requestService.AddNote(int.Parse(requestId), userId, userName, isTechnician, noteDescription);

            this.alerter.AddMessage(MessageType.Success, "Successfully added note");

            return this.RedirectToAction("Details", "Requests", new { id = requestId });
        }

        [HttpPost]
        public async Task<IActionResult> AddNoteFromTable(IEnumerable<string> ids, string noteDescription)
        {
            string referer = Request.Headers["Referer"].ToString();

            string userId = this.userManager.GetUserId(User);
            string userName = this.User.Identity.Name;
            bool isTechnician = User.IsInRole(WebConstants.AdminRole) || User.IsInRole(WebConstants.HelpdeskRole);

            await this.requestService.AddNote(ids, userId, userName, isTechnician, noteDescription);

            this.alerter.AddMessage(MessageType.Success, "Successfully added note");

            return Json(new
            {
                redirectUrl = referer
            });
        }
    }
}