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
        public IEnumerable<RequestNote> Notes { get; set; }

        public string Category { get; set; }

        public string Status { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public IEnumerable<SelectListItem> Technicians { get; set; }

        public IEnumerable<SelectListItem> Statuses { get; set; }
    }
}
