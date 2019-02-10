using BasicDesk.App.Models.Common.ViewModels;
using BasicDesk.Data.Models.Requests;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace BasicDesk.App.Models.Management.ViewModels
{
    public class RequestManagingModel
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string CreatedOn { get; set; }

        public UserDetailsViewModel Author { get; set; }

        public UserDetailsViewModel Technician { get; set; }

        public string Resolution { get; set; }

        public IEnumerable<RequestAttachment> Attachments { get; set; }
        public IEnumerable<RequestNoteViewModel> Notes { get; set; }

        public string Category { get; set; }

        public string Status { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public IEnumerable<SelectListItem> Technicians { get; set; }

        public IEnumerable<SelectListItem> Statuses { get; set; }

        public IEnumerable<RequestReplyViewModel> Replies { get; set; }

        public IEnumerable<SelectListItem> Users { get; set; }

        public IEnumerable<RequestApproval> Approvals { get; set; }
    }
}
