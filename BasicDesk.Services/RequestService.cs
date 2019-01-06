using AutoMapper;
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

        public RequestService(DbRepository<Request> repository, IMapper mapper)
        {
            this.repository = repository;
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
            switch (currentFilter)
            {
                case "MyClosed":
                    return this.repository.All()
                        .Where(r => r.RequesterId == userId)
                        .Include(r => r.Status)
                        .Where(r => r.Status.Name == "Closed" || r.Status.Name == "Rejected")
                        .Include(r => r.AssignedTo)
                        .Include(r => r.Requester).AsNoTracking();
                case "MyOpen":
                    return this.repository.All()
                        .Include(r => r.Status)
                        .Where(r => r.RequesterId == userId).Where(r => r.Status.Name != "Closed" && r.Status.Name != "Rejected")
                        .Include(r => r.AssignedTo)
                        .Include(r => r.Requester).AsNoTracking();
                default:
                    return this.GetAll(userId, isTechnician);
            }
        }

        //private async Task<RequestDetailsViewModel> GetRequestDetailsAsync(int id)
        //{
        //    var request = await this.repository.All().Where(r => r.Id == id)
        //    .Include(r => r.AssignedTo)
        //    .Include(r => r.Requester)
        //    .Include(r => r.Category)
        //    .Include(r => r.Status)
        //    .Include(r => r.Attachments)
        //    .FirstOrDefaultAsync();

        //    var requestDetails = mapper.Map<RequestDetailsViewModel>(request);

        //    if (request.AssignedTo != null)
        //    {
        //        string roles = string.Join(", ", await this.userManager.GetRolesAsync(request.AssignedTo));
        //        requestDetails.AssignedToEmail = request.AssignedTo.Email;
        //        requestDetails.AssignedToName = $"{request.AssignedTo.FullName} [{roles}]";
        //    }

        //    return requestDetails;
        //}
    }
}
