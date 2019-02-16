using System;
using System.Linq;
using AutoMapper.QueryableExtensions;
using BasicDesk.App.Models.Common.ViewModels;
using BasicDesk.Common.Constants;
using BasicDesk.Data.Models;
using BasicDesk.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

namespace BasicDesk.App.Controllers
{
    public class PartialsController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IRequestService requestService;

        public PartialsController(UserManager<User> userManager, IRequestService requestService)
        {
            this.userManager = userManager;
            this.requestService = requestService;
        }

        [HttpGet]
        [AjaxOnly]
        public ActionResult GetMergeTable(int requestId, int? page)
        {
            int pageInt = page == null ? 1 : Convert.ToInt32(page);

            string userId = userManager.GetUserId(User);

            bool isTechnician = User.IsInRole(WebConstants.AdminRole) || User.IsInRole(WebConstants.HelpdeskRole);


            var requests = this.requestService.GetAll(userId, isTechnician).Where(r => r.Id != requestId)
                  .ProjectTo<RequestMergeListingViewModel>().OrderByDescending(r => r.Id)
                  .ToArray();

            var model = new MergingTableRequestViewModel
            {
                Id = requestId,
                Requests = requests.ToPagedList(pageInt, 10)
            };


            return PartialView("MergePartial", model);
        }
    }
}