using AutoMapper;
using AutoMapper.QueryableExtensions;
using BasicDesk.App.Common;
using BasicDesk.App.Common.Attributes;
using BasicDesk.App.Common.Interfaces;
using BasicDesk.App.Helpers.Messages;
using BasicDesk.App.Models.Common;
using BasicDesk.App.Models.Common.BindingModels;
using BasicDesk.App.Models.Common.ViewModels;
using BasicDesk.Common.Constants;
using BasicDesk.Data;
using BasicDesk.Data.Models;
using BasicDesk.Data.Models.Requests;
using BasicDesk.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using X.PagedList;


namespace BasicDesk.App.Controllers
{
    [Authorize]
    public class RequestsController : Controller
    {
        private readonly BasicDeskDbContext dbContext;
        private readonly UserManager<User> userManager;
        private readonly IRequestService requestService;
        private readonly RequestSorter requestSorter;
        private readonly IAlerter alerter;
        private const int DEFAULT_PAGE_NUMBER = 1;
        private const int DEFAULT_REQUESTS_PER_PAGE = 5;


        public RequestsController(BasicDeskDbContext dbContext, UserManager<User> userManager, 
            IRequestService requestService, RequestSorter requestSorter, IAlerter alerter)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
            this.requestService = requestService;
            this.requestSorter = requestSorter;
            this.alerter = alerter;
        }

        private bool HasSearchCriteria(SearchModel searchModel)
        {
            //Using a bit of reflection to check if any of the properties are not empty
            var type = searchModel.GetType();
            var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var hasProperty = properties.Select(x => x.GetValue(searchModel, null))
                .Any(y => y != null && !String.IsNullOrWhiteSpace(y.ToString()));
            return hasProperty;
        }
        [HttpGet]
        public IActionResult Index(string sortOrder, string currentFilter, int? page, int? requestsPerPage, SearchModel searchModel)
        {
            RequestSortingViewModel model = this.requestSorter.ConfigureSorting(sortOrder, currentFilter, searchModel);

            if (model.RequestsPerPage != requestsPerPage)
            {
                model.RequestsPerPage = requestsPerPage;
            }

            var pageNumber = page ?? DEFAULT_PAGE_NUMBER;
            var requestsCount = model.RequestsPerPage ?? DEFAULT_REQUESTS_PER_PAGE;

            string userId = userManager.GetUserId(User);
            bool isTechnician = User.IsInRole(WebConstants.AdminRole) || User.IsInRole(WebConstants.HelpdeskRole);
            bool hasFilter = !String.IsNullOrEmpty(currentFilter) || currentFilter == "All";
            bool hasSearch = HasSearchCriteria(searchModel);

            IQueryable<Request> requests = Enumerable.Empty<Request>().AsQueryable();

            if (hasFilter)
            {
                requests = this.requestService.GetByFilter(userId, isTechnician, currentFilter);
            }
            if (hasSearch)
            {
                requests = this.requestService.GetBySearch(userId, isTechnician, searchModel, requests);
            }
            if (!hasFilter && !hasSearch)
            {
                requests = this.requestService.GetAll(userId, isTechnician);
            }

            var listModelRequests = requests.ProjectTo<RequestListingViewModel>();
            listModelRequests = this.requestSorter.SortRequests(listModelRequests, sortOrder);

            model.Requests = listModelRequests.ToPagedList(pageNumber, requestsCount);

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

            this.alerter.AddMessage(MessageType.Success, message);

            return this.RedirectToAction("Details", new { id = request.Id.ToString()});
        }

        [HttpPost]
        public async Task<IActionResult> Delete(IEnumerable<string> ids)
        {
            string referer = Request.Headers["Referer"].ToString();
            if (!ids.Any())
            {
                string message = "Please select request[s] for deletion";
                this.alerter.AddMessage(MessageType.Warning, message);
            }
            else
            {
                IEnumerable<int> requestIds = ids.Select(int.Parse);
                await this.requestService.Delete(requestIds);
                string message = $"Successfully deleted request[s] {string.Join(", ", ids)}";
                this.alerter.AddMessage(MessageType.Success, message);
            }

            return Json(new
            {
                redirectUrl = referer
            });
        }

        [HttpPost]
        public async Task<IActionResult> Merge(IEnumerable<string> ids)
        {
            if(ids.Any(i => int.TryParse(i, out _) == false))
            {
                return BadRequest();
            }
            if (!ids.Any())
            {
                string message = "Please select request[s] for deletion";
                this.alerter.AddMessage(MessageType.Warning, message);
            }
            else
            {
                IEnumerable<int> requestIds = ids.Select(int.Parse).OrderByDescending(i => i);

                await this.requestService.Merge(requestIds);
                await this.requestService.Delete(requestIds.SkipLast(1));
                string message = $"Successfully merged request[s] {string.Join(", ", ids)}";
                this.alerter.AddMessage(MessageType.Success, message);
            }

            bool isAjaxRequest = Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        
            if (isAjaxRequest)
            {
                string requestsTableAbsoluteUrl = Url.Action("index", "requests");
                return Json(new
                {
                    redirectUrl = requestsTableAbsoluteUrl
                });
            }
            else
            {
                return RedirectToAction("index", "requests");
            }
        }


       

        public IActionResult Details(string id)
        {
            if(int.TryParse(id, out _) == false)
            {
                return BadRequest();
            }

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

                model.ApprovalModel = new ApprovalCreationViewModel
                {
                    RequestId = requestId,
                    Users = this.userManager.Users.Select(u => new SelectListItem
                    {
                        Text = u.FullName,
                        Value = u.Id
                    })
                };

                var requests = this.requestService.GetAll(userId, isTechnician).Where(r => r.Id != requestId)
                    .ProjectTo<RequestMergeListingViewModel>()
                    .ToArray();

                return this.View(model);
            }
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
                this.alerter.AddMessage(MessageType.Danger, "File not available");

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

        private string GetContentType(string path)
        {
            var types = FileFormatValidator.GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }     
    }
}