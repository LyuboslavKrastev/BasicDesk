using BasicDesk.App.Models.Common.ViewModels;
using BasicDesk.App.Models.Management.ViewModels;
using BasicDesk.Data.Models;
using BasicDesk.Data.Models.Requests;
using BasicDesk.Services;
using BasicDesk.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace BasicDesk.App.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly IRequestService service;
        private readonly ApprovalService approvalService;

        public IndexViewModel Model { get; set; }

        public IndexModel(UserManager<User> userManager, IRequestService service, ApprovalService approvalService)
        {
            this.Model = new IndexViewModel();
            this.userManager = userManager;
            this.service = service;
            this.approvalService = approvalService;
        }

        public void OnGet()
        {
            IEnumerable <RequestStatus> statuses = service.GetAllStatuses().ToArray();
            string userId = this.userManager.GetUserId(this.User);
            Model.Reports = new List<ReportViewModel>();
            foreach (var status in statuses)
            {
                int requestsCount = this.service.GetByFilter(userId, false, status.Id.ToString()).Count();
                string requestType = status.Name;
                if (requestsCount > 0)
                {
                    Model.Reports.Add(new ReportViewModel
                    {
                        DimensionOne = requestType,
                        Quantity = requestsCount
                    });
                }
            }

            this.Model.ApprovalsToApprove = this.approvalService.GetUserApprovalsToApprove(userId).ToArray();
            this.Model.SubmittedApprovals = this.approvalService.GetUserSubmittedApprovals(userId).ToArray();

            TempData["Type"] = "pie";
        }
    }
}