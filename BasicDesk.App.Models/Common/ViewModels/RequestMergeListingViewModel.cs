using System;
using System.ComponentModel.DataAnnotations;

namespace BasicDesk.App.Models.Common.ViewModels
{
    public class RequestMergeListingViewModel
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        [Display(Name = "Requester")]
        public string Requester { get; set; }

        [Display(Name = "Assigned To")]
        public string AssignedTo { get; set; }

        [Display(Name = "Creation Time")]
        public DateTime StartTime { get; set; }

        [Display(Name = "Completion Time")]
        public DateTime? Endtime { get; set; }

        public string Status { get; set; }
    }
}
