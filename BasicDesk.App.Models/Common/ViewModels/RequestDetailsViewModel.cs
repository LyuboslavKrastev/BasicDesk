﻿using BasicDesk.App.Models.Management.ViewModels;
using BasicDesk.Data.Models.Requests;
using System.Collections.Generic;

namespace BasicDesk.App.Models.Common.ViewModels
{
    public class RequestDetailsViewModel
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string CreatedOn { get; set; }

        public string Status { get; set; }

        public string Category { get; set; }

        public string Resolution { get; set; }

        public UserDetailsViewModel Author { get; set; }

        public UserDetailsViewModel Technician { get; set; }

        public IEnumerable<RequestAttachment> Attachments { get; set; }

        public IEnumerable<RequestNoteViewModel> Notes { get; set; }

        public IEnumerable<RequestReplyViewModel> Replies { get; set; }

        public ApprovalCreationViewModel ApprovalModel { get; set; }

        public IEnumerable<RequestApprovalViewModel> Approvals { get; set; }
    }
}
