using BasicDesk.App.Models.ViewModels;
using BasicDesk.Models.Requests;
using System.Linq;

namespace BasicDesk.App.Common
{
    public class RequestSorter
    {
        public RequestSortingViewModel ConfigureSorting(string sortOrder, string currentFilter, string searchString)
        {
            var viewModel = new RequestSortingViewModel();
                viewModel.CurrentSort = sortOrder;
                viewModel.CurrentFilter = currentFilter;
                viewModel.CurrentSearch = searchString;
                
                viewModel.NameSort = sortOrder == "Name" ? "name_desc" : "Name";
                viewModel.StartDateSort = sortOrder == "StartDate" ? "startDate_desc" : "StartDate";
                viewModel.EndDateSort = sortOrder == "EndDate" ? "endDate_desc" : "EndDate";
                viewModel.IdSort = sortOrder == "Id" ? "id_desc" : "Id";
                viewModel.StatusSort = sortOrder == "Status" ? "status_desc" : "Status";
                viewModel.SubjectSort = sortOrder == "Subject" ? "subject_desc" : "Subject";
                viewModel.AssignedToSort = sortOrder == "AssignedTo" ? "assignedTo_desc" : "AssignedTo";
            return viewModel;
        }

        public IQueryable<Request> SortRequests(IQueryable<Request> requests, string sortOrder)
        {
            switch (sortOrder)
            {
                case "Name":
                    return requests.OrderBy(s => s.Requester.FullName);
                case "name_desc":
                    return requests.OrderByDescending(s => s.Requester.FullName);
                case "StartDate":
                    return requests.OrderBy(s => s.StartTime);
                case "startDate_desc":
                    return requests.OrderByDescending(s => s.StartTime);
                case "EndDate":
                    return requests.OrderBy(s => s.EndTime);
                case "endDate_desc":
                    return requests.OrderByDescending(s => s.EndTime);
                case "Id":
                    return requests.OrderBy(s => s.Id);
                case "id_desc":
                    return requests.OrderByDescending(s => s.Id);
                case "Status":
                    return requests.OrderBy(s => s.Status.Name);
                case "status_desc":
                    return requests.OrderByDescending(s => s.Status.Name);
                case "Subject":
                    return requests.OrderBy(s => s.Subject);
                case "subject_desc":
                    return requests.OrderByDescending(s => s.Subject);
                case "AssignedTo":
                    return requests.OrderBy(s => (s.AssignedTo == null) ? "" : s.AssignedTo.FullName);
                case "assignedTo_desc":
                    return requests.OrderByDescending(s => (s.AssignedTo == null) ? "" : s.AssignedTo.FullName);
                default:
                    return requests.OrderByDescending(s => s.Id);
            }
        }
    }
}
