using AutoMapper.QueryableExtensions;
using BasicDesk.App.Models.Common;
using BasicDesk.App.Models.Common.ViewModels;
using BasicDesk.App.Models.Management.BindingModels;
using BasicDesk.App.Models.Management.ViewModels;
using BasicDesk.Data.Models;
using BasicDesk.Data.Models.Requests;
using BasicDesk.Services.Interfaces;
using BasicDesk.Services.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.Services
{
    public class RequestService : IRequestService, IDbService<Request>
    {
        private readonly DbRepository<Request> repository;
        private readonly ICategoriesService categoriesService;
        private readonly DbRepository<RequestStatus> statusRepository;
        private readonly DbRepository<User> userRepository;
        private readonly DbRepository<ApprovalStatus> approvalStatusRepository;

        public RequestService(DbRepository<Request> repository, ICategoriesService categoriesService,
            DbRepository<RequestStatus> statusRepository, DbRepository<User> userRepository, DbRepository<ApprovalStatus> approvalStatusRepository)
        {
            this.repository = repository;
            this.categoriesService = categoriesService;
            this.statusRepository = statusRepository;
            this.userRepository = userRepository;
            this.approvalStatusRepository = approvalStatusRepository;
        }

        public Task AddAsync(Request request)
        {
            return this.repository.AddAsync(request);
        }

        public async Task Merge(IEnumerable<int> requestIds)
        {
            //Requests shall be merged to the lowest possible Id in the collection
            ICollection<int> ids = requestIds.SkipLast(1).ToList();
            int lastId = requestIds.Last();

            Request requestToMergeTo = await this.repository.All().FirstOrDefaultAsync(r => r.Id == lastId);

            if (requestToMergeTo == null)
            {
                return;
            }

            foreach (var id in ids)
            {
                Request request = await this.repository.All().Include(r => r.Attachments).FirstOrDefaultAsync(r => r.Id == id);

                if (request == null)
                {
                    ids.Remove(id);
                    continue;
                }

                RequestReply reply = new RequestReply
                {
                    RequestId = request.Id,
                    AuthorId = request.RequesterId,
                    Subject = request.Subject,
                    Description = request.Description,
                };

                foreach (var attachment in request.Attachments)
                {
                    reply.Attachments.Add(new ReplyAttachment
                    {
                        PathToFile = attachment.PathToFile,
                        FileName = attachment.FileName
                    });
                }

                requestToMergeTo.Repiles.Add(reply);
            }

            await this.SaveChangesAsync();
        }

        public Task SaveChangesAsync()
        {
            return this.repository.SaveChangesAsync();
        }

        public IQueryable<Request> GetAll()
        {
            return this.repository.All();
        }

        public IQueryable<Request> GetAll(string userId, bool isTechnician)
        {
            if (!isTechnician)
            {
                return this.repository.All().Where(r => r.RequesterId == userId);
            }
            return this.repository.All();
        }

        public IQueryable<Request> ById(int id)
        {
            return this.repository.All().Where(r => r.Id == id);
        }

        public IQueryable<Request> GetBySearch(string userId, bool isTechnician, SearchModel searchModel, IQueryable<Request> requests)
        {
            if (!requests.Any())
            {
                requests = GetAll(userId, isTechnician);
            }

            if (int.TryParse(searchModel.IdSearch, out int id))
            {
                requests = requests.Where(r => r.Id == id);
            }
            if (searchModel.RequesterSearch != null)
            {
                requests = requests.Where(r => r.Requester.FullName.Contains(searchModel.RequesterSearch));
            }
            if (searchModel.AssignedToSearch != null)
            {
                requests = requests.Where(r => r.AssignedTo.FullName.Contains(searchModel.AssignedToSearch));
            }
            if (searchModel.SubjectSearch != null)
            {
                requests = requests.Where(r => r.Subject.Contains(searchModel.SubjectSearch));
            }
            if (DateTime.TryParse(searchModel.CreationDateSearch, out DateTime creationDateTime))
            {
                requests = requests.Where(r => r.StartTime.Date == creationDateTime.Date);
            }
            if (DateTime.TryParse(searchModel.ClosingDateSearch, out DateTime closingDateTime))
            {

                requests = requests.Where(r => r.EndTime.Value.Date == closingDateTime.Date);
            }
            return requests.AsNoTracking();
        }

        public IQueryable<Request> GetByFilter(string userId, bool isTechnician, string currentFilter)
        {
            bool isInt = int.TryParse(currentFilter, out int statusId);
            IQueryable<Request> requests = GetAll(userId, isTechnician);

            if (isInt)
            {
                return requests.Where(r => r.Status.Id == statusId).AsNoTracking();
            }
            return requests;
        }


        public IQueryable<RequestDetailsViewModel> GetRequestDetails(int id, string userId)
        {
            IQueryable<RequestDetailsViewModel> request = this.repository.All()
                .Where(r => r.Id == id)
                .Where(r => r.RequesterId == userId)
                .ProjectTo<RequestDetailsViewModel>();

            return request.AsNoTracking();
        }

        public IQueryable<RequestManagingModel> GetRequestManagingDetails(int id)
        {
            IQueryable<RequestManagingModel> request = this.repository.All()
                .Where(r => r.Id == id)
                .ProjectTo<RequestManagingModel>();

            return request.AsNoTracking();
        }

        public async Task UpdateRequestAsync(int id, RequestEditingBindingModel model)
        {
            Request request = await this.repository.All().FirstOrDefaultAsync(r => r.Id == id);

            if (model.StatusId != null && model.StatusId != request.StatusId)
            {
                RequestStatus status = await this.GetAllStatuses().FirstOrDefaultAsync(s => s.Id == model.StatusId);
                if (status != null)
                {
                    request.StatusId = status.Id;
                }
                if (status.Name.ToLower() == "closed" || status.Name.ToLower() == "rejected")
                {
                    request.EndTime = DateTime.UtcNow;
                }
            }

            if (model.CategoryId != null && model.CategoryId != request.CategoryId)
            {
                RequestCategory category = await this.categoriesService.ById(Convert.ToInt32(model.CategoryId)).FirstAsync();
                if (category != null)
                {
                    request.CategoryId = category.Id;
                }
            }

            if (model.AssignToId != null && model.AssignToId != request.AssignedToId)
            {
                User technician = await this.userRepository.All().FirstOrDefaultAsync(s => s.Id == model.AssignToId);
                if (technician != null)
                {
                    request.AssignedToId = technician.Id;
                }
            }
            await this.SaveChangesAsync();
        }

        public async Task SaveResolutionAsync(int id, string resolution)
        {
            Request request = await this.ById(id).FirstAsync();
            request.Resolution = resolution;
            await this.SaveChangesAsync();
        }

        public async Task Delete(IEnumerable<int> requestIds)
        {
            var requests = this.repository.All()
                .Where(r => requestIds.Contains(r.Id));

            this.repository.Delete(requests);

            await this.SaveChangesAsync();
        }

        public async Task AddNote(int requestId, string userId, string userName, bool isTechnician, string noteDescription)
        {
            Request request = await this.ById(requestId).FirstAsync();

            if (isTechnician || userId == request.RequesterId)
            {
                RequestNote note = new RequestNote
                {
                    RequestId = requestId,
                    Description = noteDescription,
                    CreationTime = DateTime.UtcNow,
                    Author = userName
                };

                request.Notes.Add(note);

                await this.SaveChangesAsync();
            }
        }

        public async Task AddReply(int requestId, string userId, bool isTechnician, string noteDescription)
        {
            Request request = await this.ById(requestId).FirstAsync();

            User author = await this.userRepository.All().FirstAsync(u => u.Id == userId);

            if (isTechnician || userId == request.RequesterId)
            {

                RequestReply reply = new RequestReply
                {
                    Subject = $"Re: [{request.Subject}]",
                    RequestId = requestId,
                    Description = noteDescription,
                    CreationTime = DateTime.UtcNow,
                    Author = author
                };

                request.Repiles.Add(reply);

                await this.SaveChangesAsync();
            }
        }

        public async Task AddAproval(int requestId, string userId, bool isTechnician, string approverId, string subject, string description)
        {
            Request request = await this.repository.All().FirstOrDefaultAsync(r => r.Id == requestId);

            User author = await this.userRepository.All().FirstOrDefaultAsync(u => u.Id == userId);

            if (isTechnician || userId == request.RequesterId)
            {
                ApprovalStatus pendingStatus = await this.approvalStatusRepository.All().FirstOrDefaultAsync(s => s.Name == "Pending");

                RequestApproval approval = new RequestApproval
                {
                    Subject = subject,
                    RequestId = requestId,
                    Description = description,
                    RequesterId = userId,
                    ApproverId = approverId,
                    StatusId = pendingStatus.Id
                };

                request.Approvals.Add(approval);

                await this.SaveChangesAsync();
            }
        }


        public async Task AddNote(IEnumerable<string> requestIds, string userId, string userName, bool isTechnician, string noteDescription)
        {
            foreach (var id in requestIds)
            {
                bool isInt = int.TryParse(id, out int requestId);
                if (!isInt)
                {
                    continue;
                }
                Request request = await this.repository.All().FirstOrDefaultAsync(r => r.Id == requestId);

                if (isTechnician || userId == request.RequesterId)
                {
                    RequestNote note = new RequestNote
                    {
                        RequestId = requestId,
                        Description = noteDescription,
                        CreationTime = DateTime.UtcNow,
                        Author = userName
                    };

                    request.Notes.Add(note);
                }
            }

            await this.SaveChangesAsync();
        }


        public IQueryable<RequestStatus> GetAllStatuses()
        {
            return this.statusRepository.All().AsNoTracking();
        }
    }
}
