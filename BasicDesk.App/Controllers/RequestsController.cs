using AutoMapper;
using BasicDesk.App.Common;
using BasicDesk.App.Helpers.Messages;
using BasicDesk.App.Models.BindingModels;
using BasicDesk.App.Models.ViewModels;
using BasicDesk.Common.Constants;
using BasicDesk.Data;
using BasicDesk.Models;
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

namespace BasicDesk.App.Controllers
{
    [Authorize]
    public class RequestsController : Controller
    {
        private readonly BasicDeskDbContext dbContext;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public RequestsController(BasicDeskDbContext dbContext, IMapper mapper, UserManager<User> userManager)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.userManager = userManager;

            this.RequestSorter = new RequestSortingViewModel();
        }

        public RequestSortingViewModel RequestSorter { get; set; }

        [HttpGet]
        public IActionResult Index(string sortOrder, string searchString, string currentFilter, int? pageIndex)
        {
            ICollection<Request> requests;

            RequestSorter.CurrentSort = sortOrder;
            RequestSorter.CurrentFilter = currentFilter;
            RequestSorter.CurrentSearch = searchString;
            this.RequestSorter.ConfigureSorting(sortOrder);

            if (!String.IsNullOrEmpty(searchString))
            {
                requests = GetSearchResults(searchString);
            }
            else if (!String.IsNullOrEmpty(currentFilter))
            {
                requests = GetFilteredrequests(currentFilter);
            }
            else
            {
                requests = GetAllRequests();
            }

            requests = this.SortRequests(requests, sortOrder);


            this.RequestSorter.RequestViews = this.mapper.Map<ICollection<RequestListingViewModel>>(requests).ToArray();

            return this.View(this.RequestSorter);
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

            this.dbContext.Requests.Add(request);

            if (model.Attachment != null)
            {
                var extension = model.Attachment.FileName.Split('.').Last();
                var isAllowedFileFormat = FileFormatValidator.IsValidFormat(extension);

                if (!isAllowedFileFormat)
                {
                    this.TempData.Put("__Message", new MessageModel()
                    {
                        Type = MessageType.Danger,
                        Message = "Forbidden file type!"
                    });
                    return this.Create();
                }

                await CreateAttachmentAsync(model, request);
            }

            this.dbContext.SaveChanges();

            this.TempData.Put("__Message", new MessageModel()
            {
                Type = MessageType.Success,
                Message = "Request created successfully"
            });

            return this.RedirectToAction("Details", new { id = request.Id.ToString()});
        }


        public async Task<IActionResult> Details(string id)
        {

            var resuestDetailsViewModel =  await GetRequestDetailsAsync(int.Parse(id));

            if (User.IsInRole("Administrator"))
            {
                return this.Redirect($"/Management/Requests/Manage?id={id}");
            }
            else
            {
                return this.View(resuestDetailsViewModel);
            }
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

        private async Task CreateAttachmentAsync(RequestCreationBindingModel model, Request request)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Requests", model.Attachment.FileName);
            var fileStream = new FileStream(path, FileMode.Create);
            using (fileStream)
            {
                await model.Attachment.CopyToAsync(fileStream);
            }
            this.dbContext.RequestAttachments.Add(new RequestAttachment
            {
                FileName = model.Attachment.FileName,
                PathToFile = path,
                RequestId = request.Id
            });
        }

        private string GetContentType(string path)
        {
            var types = FileFormatValidator.GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        private ICollection<Request> SortRequests(ICollection<Request> requests, string sortOrder)
        {
            switch (sortOrder)
            {
                case "name_desc":
                    return requests.OrderByDescending(s => s.Requester.FullName).ToArray();
                case "StartDate":
                    return requests.OrderBy(s => s.StartTime).ToArray();
                case "startDate_desc":
                    return requests.OrderByDescending(s => s.StartTime).ToArray();
                case "EndDate":
                    return requests.OrderBy(s => s.EndTime).ToArray();
                case "endDate_desc":
                    return requests.OrderByDescending(s => s.EndTime).ToArray();
                case "Id":
                    return requests.OrderBy(s => s.Id).ToArray();
                case "id_desc":
                    return requests.OrderByDescending(s => s.Id).ToArray();
                case "Status":
                    return requests.OrderBy(s => s.Status.Name).ToArray();
                case "status_desc":
                    return requests.OrderByDescending(s => s.Status.Name).ToArray();
                case "Subject":
                    return requests.OrderBy(s => s.Subject).ToArray();
                case "subject_desc":
                    return requests.OrderByDescending(s => s.Subject).ToArray();
                case "AssignedTo":
                    return  requests.OrderBy(s => (s.AssignedTo == null)? "" : s.AssignedTo.FullName ).ToArray();
                case "assignedTo_desc":
                    return requests.OrderByDescending(s => (s.AssignedTo == null) ? "" : s.AssignedTo.FullName).ToArray();
                default:
                    return requests.OrderByDescending(s => s.Id).ToArray();
            }
        }

        private Request[] GetSearchResults(string searchString)
        {
            Request[] requests;

            if (User.IsInRole(WebConstants.AdminRole) || User.IsInRole(WebConstants.HelpdeskRole))
            {
                    requests = this.dbContext.Requests
                   .Where(a => a.Subject.Contains(searchString))
                   .Include(r => r.Requester)
                   .Include(r => r.Status)
                   .Include(r => r.AssignedTo)
                   .ToArray(); 
            }
            else
            {
                var userId = userManager.GetUserId(User);
                    requests = this.dbContext.Requests
                   .Where(r => r.RequesterId == userId)
                   .Where(a => a.Subject.Contains(searchString))
                   .Include(r => r.Requester)
                   .Include(r => r.AssignedTo)
                   .Include(r => r.Status)
                   .ToArray();
            }

            return requests;
        }

        private ICollection<Request> GetFilteredrequests(string currentFilter)
        {
            switch (currentFilter)
            {
                case "MyClosed":
                    var currentUserId = this.userManager.GetUserId(User);
                    return this.dbContext.Requests
                        .Where(r => r.RequesterId == currentUserId)
                        .Include(r => r.Status)
                        .Where(r => r.Status.Name == "Closed" || r.Status.Name == "Rejected")
                        .Include(r => r.AssignedTo)
                        .Include(r => r.Requester).ToArray();
                case "MyOpen":
                    currentUserId = this.userManager.GetUserId(User);
                    return this.dbContext.Requests                     
                        .Include(r => r.Status)
                        .Where(r => r.RequesterId == currentUserId).Where(r => r.Status.Name != "Closed" && r.Status.Name != "Rejected")
                        .Include(r => r.AssignedTo)
                        .Include(r => r.Requester).ToArray();
                default:
                    return GetAllRequests();
            }
        }

        private Request[] GetAllRequests()
        {
            Request[] requests;

            if(User.IsInRole(WebConstants.AdminRole) || User.IsInRole(WebConstants.HelpdeskRole))
            {
                requests = this.dbContext.Requests
               .Include(r => r.Requester)
               .Include(r => r.Status)
               .Include(r => r.AssignedTo)
               .ToArray();
            }
            else
            {
                var userId = userManager.GetUserId(User);
                requests = this.dbContext.Requests
               .Where(r => r.RequesterId == userId)
               .Include(r => r.Requester)
               .Include(r => r.AssignedTo)
               .Include(r => r.Status)
               .ToArray();
            }

            return requests;
        }
    }
}