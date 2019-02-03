using AutoMapper;
using AutoMapper.QueryableExtensions;
using BasicDesk.App.Models.Common.ViewModels;
using BasicDesk.App.Models.Management.BindingModels;
using BasicDesk.App.Models.Management.ViewModels;
using BasicDesk.Data.Models;
using BasicDesk.Data.Models.Requests;
using BasicDesk.Services.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.Services
{
    public class RequestService
    {
        private readonly DbRepository<Request> repository;
        private readonly DbRepository<RequestCategory> categoryRepository;
        private readonly DbRepository<RequestStatus> statusRepository;
        private readonly DbRepository<User> userRepository;

        public RequestService(DbRepository<Request> repository, DbRepository<RequestCategory> categoryRepository,
            DbRepository<RequestStatus> statusRepository, DbRepository<User> userRepository)
        {
            this.repository = repository;
            this.categoryRepository = categoryRepository;
            this.statusRepository = statusRepository;
            this.userRepository = userRepository;
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

        public IQueryable<RequestListingViewModel> GetAll(string userId, bool isTechnician)
        {
            IQueryable<Request> requests = GetRequestsForRole(userId, isTechnician);

            return requests.ProjectTo<RequestListingViewModel>().AsNoTracking();
        }

        public IQueryable<RequestListingViewModel> GetBySearch(string userId, bool isTechnician, string searchString)
        {
            IQueryable<Request> requests = this.GetRequestsForRole(userId, isTechnician);

            return requests.Where(a => a.Subject.Contains(searchString))
               .ProjectTo<RequestListingViewModel>().AsNoTracking();
        }

        public IQueryable<RequestListingViewModel> GetByFilter(string userId, bool isTechnician, string currentFilter)
        {
            bool isInt = int.TryParse(currentFilter, out int statusId);
            IQueryable<Request> requests = GetRequestsForRole(userId, isTechnician);

            if (isInt)
            {
                return requests.Where(r => r.Status.Id == statusId)
                    .ProjectTo<RequestListingViewModel>().AsNoTracking();
            }
            return GetAll(userId, isTechnician);
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
            }

            if (model.CategoryId != null && model.CategoryId != request.CategoryId)
            {
                RequestCategory category = await this.GetAllCategories().FirstOrDefaultAsync(s => s.Id == model.CategoryId);
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
            this.repository.All()
                .Where(r => r.Id == id)
                .FirstOrDefault()
                .Resolution = resolution;

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

        public IQueryable<RequestCategory> GetAllCategories()
        {
            return this.categoryRepository.All().AsNoTracking();
        }

        public IQueryable<RequestStatus> GetAllStatuses()
        {
            return this.statusRepository.All().AsNoTracking();
        }

        private IQueryable<Request> GetRequestsForRole(string userId, bool isTechnician)
        {
            if (!isTechnician)
            {
                return this.repository.All().Where(r => r.RequesterId == userId);
            }
            return this.repository.All();
        }
    }
}
