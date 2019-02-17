using AutoMapper.QueryableExtensions;
using BasicDesk.App.Common;
using BasicDesk.App.Common.Attributes;
using BasicDesk.App.Helpers.Messages;
using BasicDesk.App.Models.Common.ViewModels;
using BasicDesk.App.Models.Management.BindingModels;
using BasicDesk.App.Models.Management.ViewModels;
using BasicDesk.Common.Constants;
using BasicDesk.Data;
using BasicDesk.Data.Models;
using BasicDesk.Data.Models.Requests;
using BasicDesk.Services;
using BasicDesk.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.App.Areas.Management.Pages.Requests
{
    public class ManageModel : BasePageModel
    {
        private readonly UserManager<User> userManager;
        private readonly IRequestService requestService;
        private readonly ICategoriesService categoriesService;

        public RequestManagingModel ViewModel { get; set; }

        [BindProperty]
        public RequestEditingBindingModel bindingModel { get; set; }

        public ManageModel(BasicDeskDbContext dbContext, UserManager<User> userManager, IRequestService requestService, ICategoriesService categoriesService)
        {
            this.ViewModel = new RequestManagingModel();
            this.bindingModel = new RequestEditingBindingModel();
            this.userManager = userManager;
            this.requestService = requestService;
            this.categoriesService = categoriesService;
        }

        public async Task<IActionResult> OnGet(string id)
        {
            int requestId = int.Parse(id);

            await SetUpViewModel(requestId);

            return this.Page();
        }

        public async Task<IActionResult> OnPost(string id)
        {
            var status = this.requestService.GetAllStatuses().FirstOrDefault(s => s.Id == bindingModel.StatusId);

            await requestService.UpdateRequestAsync(int.Parse(id), bindingModel);

            this.TempData.Put("__Message", new MessageModel()
            {
                Type = MessageType.Success,
                Message = "Request updated successfully"
            });

            return await this.OnGet(id);
        }

        private async Task<IEnumerable<User>> GetTechnicians()
        {
            IList<User> adminUsers = await this.userManager.GetUsersInRoleAsync(WebConstants.AdminRole);
            IList<User> helpUsers = await this.userManager.GetUsersInRoleAsync(WebConstants.HelpdeskRole);

            List<User> technicians = adminUsers.ToList();
            technicians.AddRange(helpUsers.ToList());

            return technicians;
        }


        private async Task SetUpViewModel(int requestId)
        {
            this.ViewModel = await this.requestService.GetRequestManagingDetails(requestId)
                .FirstOrDefaultAsync();

            this.ViewModel.Categories = this.categoriesService.GetAll().ProjectTo<SelectListItem>();
            this.ViewModel.Statuses = this.requestService.GetAllStatuses().ProjectTo<SelectListItem>();

            var technicians = await this.GetTechnicians();
            this.ViewModel.Technicians = technicians.Select(t => new SelectListItem
            {
                Value = t.Id,
                Text = t.UserName
            });

            ViewModel.ApprovalModel = new ApprovalCreationViewModel
            {
                RequestId = requestId,
                Users = this.userManager.Users.Select(u => new SelectListItem
                {
                    Text = u.FullName,
                    Value = u.Id
                })
            };
        }
    }
}