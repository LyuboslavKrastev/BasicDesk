using AutoMapper;
using BasicDesk.App.Common;
using BasicDesk.App.Helpers.Messages;
using BasicDesk.App.Models.BindingModels;
using BasicDesk.App.Models.ViewModels;
using BasicDesk.Common.Constants;
using BasicDesk.Data;
using BasicDesk.Models;
using BasicDesk.Models.Requests;
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
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private RequestService requestService;
        private readonly RequestSorter requestSorter;
        private string userId;
        private bool isTechnician;
        private const int DEFAULT_PAGE_NUMBER = 1;
        private const int DEFAULT_REQUESTS_PER_PAGE = 10;


        public RequestsController(BasicDeskDbContext dbContext, IMapper mapper, UserManager<User> userManager, 
            RequestService requestService, RequestSorter requestSorter)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;
            this.requestService = requestService;
            this.requestSorter = requestSorter;
        }

        public RequestSortingViewModel viewModel { get; set; }

        [HttpGet]
        public IActionResult Index(string sortOrder, string searchString, string currentFilter, int? page, int? requestsPerPage)
        {
            this.viewModel = this.requestSorter.ConfigureSorting(sortOrder, currentFilter, searchString);
            var pageNumber = page ?? DEFAULT_PAGE_NUMBER;
            var requestsCount = requestsPerPage ?? DEFAULT_REQUESTS_PER_PAGE;

            userId = userManager.GetUserId(User);
            isTechnician = User.IsInRole(WebConstants.AdminRole) || User.IsInRole(WebConstants.HelpdeskRole);

            IQueryable<Request> requests;      

            if (!String.IsNullOrEmpty(searchString))
            {
                requests = this.requestService.GetBySearch(userId, isTechnician, searchString);
            }
            else if (!String.IsNullOrEmpty(currentFilter))
            {
                requests = this.requestService.GetByFilter(userId, isTechnician, currentFilter);
            }
            else
            {
                requests = this.requestService.GetAll(userId, isTechnician);
            }

            requests = this.requestSorter.SortRequests(requests, sortOrder);


            var requestViewModels = this.mapper.Map<ICollection<RequestListingViewModel>>(requests).ToPagedList(pageNumber, requestsCount);

            this.viewModel.RequestListingViewModels = requestViewModels;

            return this.View(this.viewModel);
        }

        public IActionResult Create()
        {
            var requestBindingModel = new RequestCreationBindingModel();
            var requestCategories = dbContext.RequestCategories.ToArray();

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

            var request = this.mapper.Map<Request>(model);
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

            this.TempData.Put("__Message", new MessageModel()
            {
                Type = MessageType.Success,
                Message = message
            });

            return this.RedirectToAction("Details", new { id = request.Id.ToString()});
        }


        public async Task<IActionResult> Details(string id)
        {

            var resuestDetailsViewModel =  await GetRequestDetailsAsync(int.Parse(id));

            if (User.IsInRole("Administrator") || User.IsInRole("HelpdeskAgent"))
            {
                return this.Redirect($"/Management/Requests/Manage?id={id}");
            }
            else
            {
                return this.View(resuestDetailsViewModel);
            }
        }

        [HttpPost]
        public IActionResult SaveResolution(int reqId, string resol)
        {
            var req = this.dbContext.Requests.FirstOrDefault(r => r.Id == reqId);

            req.Resolution = resol;

            dbContext.SaveChanges();

            this.TempData.Put("__Message", new MessageModel()
            {
                Type = MessageType.Success,
                Message = $"Successfully saved resolution for request {reqId}"
            });
            return this.Redirect($"/Management/Requests/Manage?id={reqId}");
        }

        private async Task<RequestDetailsViewModel> GetRequestDetailsAsync (int id)
        {
            var request = await this.dbContext.Requests
            .Include(r => r.AssignedTo)
            .Include(r => r.Requester)
            .Include(r =>r.Category)
            .Include(r => r.Status)
            .Include(r => r.Attachments)
            .FirstOrDefaultAsync(r => r.Id == id);

            var requestDetails = mapper.Map<RequestDetailsViewModel>(request);

            if (request.AssignedTo != null)
            {
                string roles = string.Join(", ", await this.userManager.GetRolesAsync(request.AssignedTo));
                requestDetails.AssignedToEmail = request.AssignedTo.Email;
                requestDetails.AssignedToName = $"{request.AssignedTo.FullName} [{roles}]";
            }

            return requestDetails;
        }

        public async Task<IActionResult> Download(string fileName, string filePath, string requestId)
        {   
            try
            {
                if (fileName == null)
                    return Content("filename not present");

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
                this.TempData.Put("__Message", new MessageModel()
                {
                    Type = MessageType.Danger,
                    Message = "File not available"
            });

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