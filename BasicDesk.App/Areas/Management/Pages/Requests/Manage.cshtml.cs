using BasicDesk.App.Areas.Management.Models.BindingModels;
using BasicDesk.App.Areas.Management.Models.ViewModels;
using BasicDesk.App.Common;
using BasicDesk.App.Helpers.Messages;
using BasicDesk.Common.Constants;
using BasicDesk.Data;
using BasicDesk.Models;
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
        private readonly BasicDeskDbContext dbContext;
        private readonly UserManager<User> userManager;

        public RequestManagingModel ViewModel { get; set; }

        [BindProperty]
        public RequestEditingBindingModel bindingModel { get; set; }

        public ManageModel(BasicDeskDbContext dbContext, UserManager<User> userManager)
        {
            this.ViewModel = new RequestManagingModel();
            this.bindingModel = new RequestEditingBindingModel();
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public async Task<IActionResult> OnGet(string id)
        {
            var request = await GetRequestDetails(id);
            var requestCategories = await GetRequestCategories();

            foreach (var requestCategory in requestCategories)
            {
                this.ViewModel.Categories.Add(new SelectListItem
                {
                    Text = requestCategory.Name,
                    Value = requestCategory.Id.ToString()
                });
            }

            this.ViewModel.Id = request.Id;
            this.ViewModel.Subject = request.Subject;
            this.ViewModel.Description = request.Description;
            this.ViewModel.CreatedOn = request.StartTime.ToString();
            this.ViewModel.Status = request.Status.Name;
            this.ViewModel.Attachment = request.Attachments.FirstOrDefault();
            this.ViewModel.Author = request.Requester.FullName;
            this.ViewModel.Category = request.Category.Name;
            this.ViewModel.AuthorId = request.RequesterId;

            if (request.AssignedTo != null)
            {
                string roles = string.Join(", ", await this.userManager.GetRolesAsync(request.AssignedTo));
                this.ViewModel.AssignedToEmail = request.AssignedTo.Email;
                this.ViewModel.AssignedToName = $"{request.AssignedTo.FullName} [{roles}]";
            }

            SetStatusSelectList(request);

            await SetEmployeeSelectList(request);

            return this.Page();
        }

        private async Task<ICollection<RequestCategory>> GetRequestCategories()
        {
            return await dbContext.RequestCategories.ToArrayAsync();
        }

        private async Task<Request> GetRequestDetails(string id)
        {
            return await this.dbContext.Requests
                .Include(r => r.AssignedTo)
                .Include(r => r.Status)
                .Include(r => r.Attachments)
                .Include(r => r.Requester)
                .Include(r => r.Category)
                .FirstOrDefaultAsync(r => r.Id == int.Parse(id));
        }

        public Task<IActionResult> OnPost(string id)
        {
            var closedStatusId = this.dbContext.RequestStatuses.FirstOrDefault(s => s.Name == "Closed").Id;

            var request = this.dbContext.Requests.Find(int.Parse(id));

            request.StatusId = bindingModel.StatusId;
            request.AssignedToId = bindingModel.AssignToId;
            request.CategoryId = bindingModel.CategoryId;

            if (request.StatusId == closedStatusId)
            {
                request.EndTime = DateTime.Now;
            }

            dbContext.SaveChanges();

            this.TempData.Put("__Message", new MessageModel()
            {
                Type = MessageType.Success,
                Message = "Request updated successfully"
            });

            return this.OnGet(id);
        }

        private void SetStatusSelectList(Request request)
        {
            var statuses = this.dbContext.RequestStatuses.ToArray();

            if (request.Status != null)
            {
                this.ViewModel.Statuses.Add(new SelectListItem
                {
                    Text = request.Status.Name,
                    Value = request.StatusId.ToString()
                });

                foreach (var status in statuses.Where(s => s.Name != request.Status.Name))
                {
                    this.ViewModel.Statuses.Add(new SelectListItem
                    {
                        Text = status.Name,
                        Value = status.Id.ToString()
                    });
                }
            }
            else
            {
                foreach (var status in statuses)
                {
                    this.ViewModel.Statuses.Add(new SelectListItem
                    {
                        Text = status.Name,
                        Value = status.Id.ToString()
                    });
                }
            }
        }

        private async Task SetEmployeeSelectList(Request request)
        {
            var adminUsers = await this.userManager.GetUsersInRoleAsync(WebConstants.AdminRole);
            var helpUsers = await this.userManager.GetUsersInRoleAsync(WebConstants.HelpdeskRole);

            if (!string.IsNullOrWhiteSpace(request.AssignedToId))
            {
                this.ViewModel.Employees.Add(new SelectListItem
                {
                    Text = request.AssignedTo.FullName,
                    Value = request.AssignedToId
                });
            }

            foreach (var admin in adminUsers.Where(a => a.Id != request.AssignedToId))
            {
                this.ViewModel.Employees.Add(new SelectListItem
                {
                    Text = $"{admin.FullName}",
                    Value = admin.Id
                });
            }

            foreach (var hdUser in helpUsers.Where(h => h.Id != request.AssignedToId))
            {
                this.ViewModel.Employees.Add(new SelectListItem
                {
                    Text = $"{hdUser.FullName}",
                    Value = hdUser.Id
                });
            }
        }
    }
}