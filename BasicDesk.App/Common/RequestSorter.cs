using BasicDesk.App.Models.Common;
using BasicDesk.App.Models.Common.ViewModels;
using System.Linq;

namespace BasicDesk.App.Common
{
    public class RequestSorter
    {
        public RequestSortingViewModel ConfigureSorting(string sortOrder, string currentFilter, SearchModel searchModel)
        {
            var viewModel = new RequestSortingViewModel
            {
                CurrentSort = sortOrder,
                CurrentFilter = currentFilter,
                CurrentSearch = searchModel,

                NameSort = sortOrder == "Name" ? "name_desc" : "Name",
                StartDateSort = sortOrder == "StartDate" ? "startDate_desc" : "StartDate",
                EndDateSort = sortOrder == "EndDate" ? "endDate_desc" : "EndDate",
                IdSort = sortOrder == "Id" ? "id_desc" : "Id",
                StatusSort = sortOrder == "Status" ? "status_desc" : "Status",
                SubjectSort = sortOrder == "Subject" ? "subject_desc" : "Subject",
                AssignedToSort = sortOrder == "AssignedTo" ? "assignedTo_desc" : "AssignedTo"
            };
            return viewModel;
        }

        public IQueryable<RequestListingViewModel> SortRequests(IQueryable<RequestListingViewModel> requests, string sortOrder)
        {
            switch (sortOrder)
            {
                case "Name":
                    return requests.OrderBy(s => s.Requester);
                case "name_desc":
                    return requests.OrderByDescending(s => s.Requester);
                case "StartDate":
                    return requests.OrderBy(s => s.StartTime);
                case "startDate_desc":
                    return requests.OrderByDescending(s => s.StartTime);
                case "EndDate":
                    return requests.OrderBy(s => s.Endtime);
                case "endDate_desc":
                    return requests.OrderByDescending(s => s.Endtime);
                case "Id":
                    return requests.OrderBy(s => s.Id);
                case "id_desc":
                    return requests.OrderByDescending(s => s.Id);
                case "Status":
                    return requests.OrderBy(s => s.Status);
                case "status_desc":
                    return requests.OrderByDescending(s => s.Status);
                case "Subject":
                    return requests.OrderBy(s => s.Subject);
                case "subject_desc":
                    return requests.OrderByDescending(s => s.Subject);
                case "AssignedTo":
                    return requests.OrderBy(s => s.AssignedTo ?? "");
                case "assignedTo_desc":
                    return requests.OrderByDescending(s => s.AssignedTo ?? "");
                default:
                    return requests.OrderByDescending(s => s.Id);
            }
        }
    }
}
