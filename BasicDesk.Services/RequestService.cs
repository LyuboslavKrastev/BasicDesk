using AutoMapper;
using AutoMapper.QueryableExtensions;
using BasicDesk.App.Models.Common.ViewModels;
using BasicDesk.Common.Constants;
using BasicDesk.Data;
using BasicDesk.Data.Models.Requests;
using BasicDesk.Services.Repository;
using Microsoft.AspNetCore.Identity;
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

        public Task SaveResolutionAsync(int id, string resolution)
        {
            this.repository.All()
                .Where(r => r.Id == id)
                .FirstOrDefault()
                .Resolution = resolution;

            return this.SaveChangesAsync();
        }

        public Task Delete(IEnumerable<int> requestIds)
        {
            var requests = this.repository.All()
                .Where(r => requestIds.Contains(r.Id));

            this.repository.Delete(requests);

            return this.SaveChangesAsync();
        }

        public Task AddNote(IEnumerable<int> requestIds)
        {
            var requests = this.repository.All()
                .Where(r => requestIds.Contains(r.Id));

            //TODO
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
