using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicDesk.App.Common;
using BasicDesk.App.Common.Interfaces;
using BasicDesk.App.Helpers.Messages;
using BasicDesk.App.Models.Common.BindingModels;
using BasicDesk.Common.Constants;
using BasicDesk.Data.Models;
using BasicDesk.Services;
using BasicDesk.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BasicDesk.App.Controllers
{
    public class ApprovalsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IApprovalService approvalService;
        private readonly IRequestService requestService;
        private readonly IAlerter alerter;

        public ApprovalsController(UserManager<User> userManager, IApprovalService approvalService, 
            IRequestService requestService, IAlerter alerter)
        {
            this.userManager = userManager;
            this.approvalService = approvalService;
            this.requestService = requestService;
            this.alerter = alerter;
        }

        [HttpPost]
        public async Task<IActionResult> AddApproval(ApprovalCreationBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                this.alerter.AddMessage(MessageType.Danger, "Invalid approval data");
                return this.RedirectToAction("Details", new { id = model.RequestId });
            }

            string userId = this.userManager.GetUserId(User);

            bool isTechnician = User.IsInRole(WebConstants.AdminRole) || User.IsInRole(WebConstants.HelpdeskRole);

            await this.requestService.AddAproval(model.RequestId, userId, isTechnician, model.ApproverId, model.Subject, model.Description);

            this.alerter.AddMessage(MessageType.Success, "Successfully submitted for approval");

            return this.RedirectToAction("Details", "Requests", new { id = model.RequestId });
        }

        [HttpPost]
        public async Task<IActionResult> ApproveApproval(string requestId, string approvalId)
        {
            if (!int.TryParse(approvalId, out int appId) || !int.TryParse(requestId, out _))
            {
                return BadRequest();
            }

            string userId = this.userManager.GetUserId(User);

            await this.approvalService.ApproveApproval(appId, userId);

            this.alerter.AddMessage(MessageType.Success, "Successfully approved approval");

            return this.RedirectToAction("Details", "Requests", new { id = requestId });
        }

        [HttpPost]
        public async Task<IActionResult> DenyApproval(string requestId, string approvalId)
        {
            if (!int.TryParse(approvalId, out int appId) || !int.TryParse(requestId, out _))
            {
                return BadRequest();
            }

            string userId = this.userManager.GetUserId(User);

            await this.approvalService.DenyApproval(appId, userId);

            this.alerter.AddMessage(MessageType.Success, "Successfully denied approval");

            return this.RedirectToAction("Details", "Requests", new { id = requestId });
        }
    }
}