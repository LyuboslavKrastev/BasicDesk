using System;

namespace BasicDesk.App.Models.ViewModels
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

        public RequestListingViewModel [] RequestViews { get; set; }

        public void ConfigureSorting(string sortOrder)
        {
            this.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            this.StartDateSort = sortOrder == "StartDate" ? "startDate_desc" : "StartDate";
            this.EndDateSort = sortOrder == "EndDate" ? "endDate_desc" : "EndDate";
            this.IdSort = sortOrder == "Id" ? "id_desc" : "Id";
            this.StatusSort = sortOrder == "Status" ? "status_desc" : "Status";
            this.SubjectSort = sortOrder == "Subject" ? "subject_desc" : "Subject";
            this.AssignedToSort = sortOrder == "AssignedTo" ? "assignedTo_desc" : "AssignedTo";
        }
    }

   
}
