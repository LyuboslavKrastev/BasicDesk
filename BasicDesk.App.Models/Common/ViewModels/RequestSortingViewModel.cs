using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
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
        public SearchModel CurrentSearch { get; set; }

        public int? RequestsPerPage { get; set; }

        public IEnumerable<SelectListItem> Statuses { get; set; }

        public IEnumerable<SelectListItem> ReqPerPageList { get; set; } = new List<SelectListItem>
        {
            new SelectListItem
            {
                Value = "5",
                Text = "5"
            },
            new SelectListItem
            {
                Value = "10",
                Text = "10"
            },
            new SelectListItem
            {
                Value = "25",
                Text = "25"
            },
            new SelectListItem
            {
                Value = "50",
                Text = "50"
            },
            new SelectListItem
            {
                Value = "100",
                Text = "100"
            },
            new SelectListItem
            {
                Value = "150",
                Text = "150"
            },
            new SelectListItem
            {
                Value = "200",
                Text = "200"
            }
        };

        public IPagedList<RequestListingViewModel> Requests { get; set; }
    } 
}
