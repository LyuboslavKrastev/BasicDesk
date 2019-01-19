using AutoMapper;
using BasicDesk.App.Models.Common.ViewModels;
using BasicDesk.Common.Constants;
using BasicDesk.Data;
using BasicDesk.Data.Models.Requests;
using BasicDesk.Services.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.Services
{
    public class RequestService
    {
        private readonly DbRepository<Request> repository;
        private readonly DbRepository<RequestCategory> categoryRepository;
        private readonly DbRepository<RequestStatus> statusRepository;

        public RequestService(DbRepository<Request> repository, DbRepository<RequestCategory> categoryRepository, DbRepository<RequestStatus> statusRepository)
        {
            this.repository = repository;
            this.categoryRepository = categoryRepository;
            this.statusRepository = statusRepository;
        }

        public Task AddAsync(Request request)
        {
            return this.repository.AddAsync(request);
        }
        public Task SaveChangesAsync()
        {
            return this.repository.SaveChangesAsync();
        }

        public IQueryable<Request> GetAll(string userId, bool isTechnician)
        {
            if (isTechnician)
            {
                return this.repository.All()
                   .Include(r => r.Requester)
                   .Include(r => r.Status)
                   .Include(r => r.AssignedTo).AsNoTracking();
            }
            return this.repository.All()
               .Where(r => r.RequesterId == userId)
               .Include(r => r.Requester)
               .Include(r => r.AssignedTo)
               .Include(r => r.Status).AsNoTracking();
        }

        public IQueryable<Request> GetBySearch(string userId, bool isTechnician, string searchString)
        {
            if (isTechnician)
            {
                return this.repository.All()
               .Where(a => a.Subject.Contains(searchString))
               .Include(r => r.Requester)
               .Include(r => r.Status)
               .Include(r => r.AssignedTo).AsNoTracking();
            }

            return this.repository.All()
               .Where(r => r.RequesterId == userId)
               .Where(a => a.Subject.Contains(searchString))
               .Include(r => r.Requester)
               .Include(r => r.AssignedTo)
               .Include(r => r.Status).AsNoTracking();
        }

        public IQueryable<Request> GetByFilter(string userId, bool isTechnician, string currentFilter)
        {
            bool isInt = int.TryParse(currentFilter, out int statusId);

            if (isInt)
            {
                return this.repository.All()
                            .Where(r => r.RequesterId == userId)
                            .Include(r => r.Status)
                            .Where(r => r.Status.Id == statusId)
                            .Include(r => r.AssignedTo)
                            .Include(r => r.Requester).AsNoTracking();
            }
            else
            {
                return this.repository.All()
                            .Where(r => r.RequesterId == userId)
                            .Include(r => r.Status)
                            .Include(r => r.AssignedTo)
                            .Include(r => r.Requester).AsNoTracking();
            }

        }

        public async Task<RequestDetailsViewModel> GetRequestDetailsAsync(int id)
        {
            var request = await this.repository.All().Where(r => r.Id == id)
            .Include(r => r.AssignedTo)
            .Include(r => r.Requester)
            .Include(r => r.Category)
            .Include(r => r.Status)
            .Include(r => r.Attachments)
            .FirstOrDefaultAsync();

            var requestDetails = Mapper.Map<RequestDetailsViewModel>(request);

            if (request.AssignedTo != null)
            {
                requestDetails.AssignedToEmail = request.AssignedTo.Email;
                requestDetails.AssignedToName = request.AssignedTo.FullName;
            }

            return requestDetails;
        }

        public Task SaveResolutionAsync(int id, string resolution)
        {
            var req = this.repository.All()
                .Where(r => r.Id == id)
                .FirstOrDefault();

            req.Resolution = resolution;

            return this.SaveChangesAsync();
        }

        public IQueryable<RequestCategory> GetAllCategories()
        {
            return this.categoryRepository.All().AsNoTracking();
        }

        public IQueryable<RequestStatus> GetAllStatuses()
        {
                return this.statusRepository.All().AsNoTracking();
        }
    }
}
