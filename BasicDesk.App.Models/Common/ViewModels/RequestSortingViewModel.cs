using System;
using X.PagedList;

namespace BasicDesk.App.Models.Common.ViewModels
{
    public class RequestSortingViewModel
    {
        public string IdSort { get; set; }
        public string SubjectSort { get; set; }
        public string NameSort { get; set; }
        public string StartDateSort { get; set; }
        public string EndDateSort { get; set; }
        public string AssignedToSort { get; set; }
        public string StatusSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public string CurrentSearch { get; set; }

        public IPagedList<RequestListingViewModel> RequestListingViewModels { get; set; }
    } 
}
