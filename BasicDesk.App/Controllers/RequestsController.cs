using AutoMapper;
using BasicDesk.App.Common;
using BasicDesk.App.Helpers.Messages;
using BasicDesk.App.Models.Common.BindingModels;
using BasicDesk.App.Models.Common.ViewModels;
using BasicDesk.Common.Constants;
using BasicDesk.Data;
using BasicDesk.Data.Models;
using BasicDesk.Data.Models.Requests;
using BasicDesk.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace BasicDesk.App.Controllers
{
    [Authorize]
    public class RequestsController : Controller
    {
        private readonly BasicDeskDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly RequestService requestService;
        private readonly RequestSorter requestSorter;
        private const int DEFAULT_PAGE_NUMBER = 1;
        private const int DEFAULT_REQUESTS_PER_PAGE = 5;


        public RequestsController(BasicDeskDbContext dbContext, UserManager<User> userManager, 
            RequestService requestService, RequestSorter requestSorter)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.requestService = requestService;
            this.requestSorter = requestSorter;
        }

        [HttpGet]
        public IActionResult Index(string sortOrder, string searchString, string currentFilter, int? page, int? requestsPerPage)
        {
            RequestSortingViewModel model = this.requestSorter.ConfigureSorting(sortOrder, currentFilter, searchString);

            if(model.RequestsPerPage != requestsPerPage)
            {
                model.RequestsPerPage = requestsPerPage;
            }

            var pageNumber = page ?? DEFAULT_PAGE_NUMBER;
            var requestsCount = model.RequestsPerPage ?? DEFAULT_REQUESTS_PER_PAGE;

            string userId = userManager.GetUserId(User);
            bool isTechnician = User.IsInRole(WebConstants.AdminRole) || User.IsInRole(WebConstants.HelpdeskRole);

            IQueryable<RequestListingViewModel> requests;      

            if (!String.IsNullOrEmpty(searchString))
            {
                requests = this.requestService.GetBySearch(userId, isTechnician, searchString);
            }
            else if (!String.IsNullOrEmpty(currentFilter) || currentFilter == "All")
            {
                requests = this.requestService.GetByFilter(userId, isTechnician, currentFilter);
            }
            else
            {
                requests = this.requestService.GetAll(userId, isTechnician);
            }

            requests = this.requestSorter.SortRequests(requests, sortOrder);

            model.Requests = requests.ToPagedList(pageNumber, requestsCount);

            model.Statuses = this.requestService.GetAllStatuses().Select(s => new SelectListItem
            {
                Text = s.Name,
                Value = s.Id.ToString()
            }).ToArray();


            return this.View(model);
        }

        public IActionResult Create()
        {
            var requestBindingModel = new RequestCreationBindingModel();
            var requestCategories = this.requestService.GetAllCategories().ToArray();

            foreach (var requestCategory in requestCategories)
            {
                requestBindingModel.Categories.Add(new SelectListItem
                {
                    Text = requestCategory.Name,
                    Value = requestCategory.Id.ToString()
                });
            }

            return this.View(requestBindingModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RequestCreationBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var request = Mapper.Map<Request>(model);
            var openStatusId = this.dbContext.RequestStatuses.FirstOrDefault(s => s.Name == "Open").Id;
            request.RequesterId = this.userManager.GetUserId(User);
            request.StatusId = openStatusId;

            await this.requestService.AddAsync(request);

            string failedAttachments = string.Empty;

            if (model.Attachments != null)
            {
                failedAttachments = await CreateAttachmentAsync(model, request);
            }

            string message = "Request created successfully";
            if(failedAttachments != string.Empty)
            {
                message += failedAttachments;
            }

            await this.requestService.SaveChangesAsync();

            this.AddMessage(MessageType.Success, message);

            return this.RedirectToAction("Details", new { id = request.Id.ToString()});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(IEnumerable<string> ids)
        {
            string referer = Request.Headers["Referer"].ToString();
            if (!ids.Any())
            {
                string message = "Please select request[s] for deletion";
                this.AddMessage(MessageType.Warning, message);
            }
            else
            {
                IEnumerable<int> requestIds = ids.Select(int.Parse);
                await this.requestService.Delete(requestIds);
                string message = $"Successfully deleted request[s] {string.Join(", ", ids)}";
                this.AddMessage(MessageType.Success, message);
            }

            return Json(new
            {
                redirectUrl = referer
            });
        }

        [HttpPost]
        public async Task<IActionResult> Merge(IEnumerable<string> ids)
        {
            string referer = Request.Headers["Referer"].ToString();
            if (!ids.Any())
            {
                string message = "Please select request[s] for deletion";
                this.AddMessage(MessageType.Warning, message);
            }
            else
            {
                IEnumerable<int> requestIds = ids.Select(int.Parse).OrderByDescending(i => i);

                await this.requestService.Merge(requestIds);
                await this.requestService.Delete(requestIds.SkipLast(1));
                string message = $"Successfully merged request[s] {string.Join(", ", ids)}";
                this.AddMessage(MessageType.Success, message);
            }

            return Json(new
            {
                redirectUrl = referer
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddNote(string requestId, string noteDescription)
        {
            string userId = this.userManager.GetUserId(User);
            string userName = this.User.Identity.Name;
            bool isTechnician = User.IsInRole(WebConstants.AdminRole) || User.IsInRole(WebConstants.HelpdeskRole);

            await this.requestService.AddNote(int.Parse(requestId), userId, userName, isTechnician, noteDescription);

            this.AddMessage(MessageType.Success, "Successfully added note");

            return this.RedirectToAction("Details", new { id = requestId });
        }

        [HttpPost]
        public async Task<IActionResult> AddNoteFromTable(IEnumerable<string> ids, string noteDescription)
        {
            string referer = Request.Headers["Referer"].ToString();

            string userId = this.userManager.GetUserId(User);
            string userName = this.User.Identity.Name;
            bool isTechnician = User.IsInRole(WebConstants.AdminRole) || User.IsInRole(WebConstants.HelpdeskRole);

            await this.requestService.AddNote(ids, userId, userName, isTechnician, noteDescription);

            this.AddMessage(MessageType.Success, "Successfully added note");

            return Json(new
            {
                redirectUrl = referer
            });
        }

        public IActionResult Details(string id)
        {
            bool isTechnician = User.IsInRole(WebConstants.AdminRole) || User.IsInRole(WebConstants.HelpdeskRole);
            if (isTechnician)
            {
                return this.Redirect($"/Management/Requests/Manage?id={id}");
            }
            else
            {
                int requestId = int.Parse(id);
                string userId = this.userManager.GetUserId(User);

                RequestDetailsViewModel model = this.requestService.GetRequestDetails(requestId, userId)
                    .FirstOrDefault();
                
                //If the request that the user is trying to access, is not his own, the model shall be null
                if(model == null)
                {
                    return this.Forbid();
                }

                return this.View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveResolution(int reqId, string resol)
        {
            await this.requestService.SaveResolutionAsync(reqId, resol);

            this.AddMessage(MessageType.Success, $"Successfully saved resolution for request {reqId}");

            return this.Redirect($"/Management/Requests/Manage?id={reqId}");
        }

        public async Task<IActionResult> Download(string fileName, string filePath, string requestId)
        {   
            try
            {
                if (fileName == null)
                {
                    return Content("filename not present");
                }

                var memory = new MemoryStream();
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, GetContentType(filePath), Path.GetFileName(filePath));
            }
            catch (IOException)
            {
                this.AddMessage(MessageType.Danger, "File not available");

                return this.RedirectToAction("Details", new { id = requestId});
            }
        }

        private async Task<string> CreateAttachmentAsync(RequestCreationBindingModel model, Request request)
        {
            string failedAttachments = string.Empty;
            foreach (var attachment in model.Attachments)
            {
                var extension = attachment.FileName.Split('.').Last();
                var isAllowedFileFormat = FileFormatValidator.IsValidFormat(extension);

                if (!isAllowedFileFormat)
                {
                    failedAttachments += Environment.NewLine;
                    failedAttachments += $"{attachment.FileName} failed to upload because the file format is forbidden.";               
                    continue;
                }
                string currentDirectory = Directory.GetCurrentDirectory();
                string destination = currentDirectory + "/Files" + "/Requests/" + model.Subject;
                Directory.CreateDirectory(destination);
                string path = Path.Combine(destination, attachment.FileName);
                var fileStream = new FileStream(path, FileMode.Create);
                using (fileStream)
                {
                    await attachment.CopyToAsync(fileStream);
                }
                this.dbContext.RequestAttachments.Add(new RequestAttachment
                {
                    FileName = attachment.FileName,
                    PathToFile = path,
                    RequestId = request.Id
                });

            }
            return failedAttachments;
        }

        private void AddMessage(MessageType type, string message)
        {
            this.TempData.Put("__Message", new MessageModel()
            {
                Type = type,
                Message = message
            });
        }

        private string GetContentType(string path)
        {
            var types = FileFormatValidator.GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }     
    }
}