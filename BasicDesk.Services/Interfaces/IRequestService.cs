using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicDesk.App.Models.Common;
using BasicDesk.App.Models.Common.ViewModels;
using BasicDesk.App.Models.Management.BindingModels;
using BasicDesk.App.Models.Management.ViewModels;
using BasicDesk.Data.Models.Requests;

namespace BasicDesk.Services.Interfaces
{
    public interface IRequestService
    {
        Task AddAproval(int requestId, string userId, bool isTechnician, string approverId, string subject, string description);
        Task AddAsync(Request request);
        Task AddNote(IEnumerable<string> requestIds, string userId, string userName, bool isTechnician, string noteDescription);
        Task AddNote(int requestId, string userId, string userName, bool isTechnician, string noteDescription);
        Task AddReply(int requestId, string userId, bool isTechnician, string noteDescription);
        Task Delete(IEnumerable<int> requestIds);
        IQueryable<Request> GetAll(string userId, bool isTechnician);
        IQueryable<RequestCategory> GetAllCategories();
        IQueryable<RequestStatus> GetAllStatuses();
        IQueryable<Request> GetByFilter(string userId, bool isTechnician, string currentFilter);
        IQueryable<Request> GetBySearch(string userId, bool isTechnician, SearchModel searchModel, IQueryable<Request> requests);
        IQueryable<RequestDetailsViewModel> GetRequestDetails(int id, string userId);
        IQueryable<RequestManagingModel> GetRequestManagingDetails(int id);
        Task Merge(IEnumerable<int> requestIds);
        Task SaveChangesAsync();
        Task SaveResolutionAsync(int id, string resolution);
        Task UpdateRequestAsync(int id, RequestEditingBindingModel model);
    }
}