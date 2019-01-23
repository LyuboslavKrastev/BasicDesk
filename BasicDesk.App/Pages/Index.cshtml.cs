using BasicDesk.App.Models.Management.ViewModels;
using BasicDesk.Data.Models;
using BasicDesk.Data.Models.Requests;
using BasicDesk.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace BasicDesk.App.Pages
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly RequestService service;

        public IList<ReportViewModel> Model { get; set; }

        public IndexModel(UserManager<User> userManager, RequestService service)
        {
            this.Model = new List<ReportViewModel>();
            this.userManager = userManager;
            this.service = service;
        }

        public void OnGet()
        {
            IEnumerable <RequestStatus> statuses = service.GetAllStatuses().ToArray();
            string userId = this.userManager.GetUserId(this.User);

            foreach (var status in statuses)
            {

                int quantity = this.service.GetByFilter(userId, false, status.Id.ToString()).Count();
                string requestType = status.Name;
                if (quantity > 0)
                {
                    Model.Add(new ReportViewModel
                    {
                        DimensionOne = requestType,
                        Quantity = quantity
                    });
                }
            }

            TempData["Type"] = "pie";
        }
    }
}