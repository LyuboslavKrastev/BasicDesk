using BasicDesk.Common.Constants;
using BasicDesk.Common.Constants.Validation;
using BasicDesk.Data.Models.Interfaces;
using BasicDesk.Data.Models.Solution;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BasicDesk.Data.Models.Requests
{
    public class Request : IEntity
    {
        public int Id { get; set; }

        [Required]
        [MinLength(RequestConstants.SubjectMinLength)]
        [MaxLength(RequestConstants.SubjectMaxLength)]
        public string Subject { get; set; }

        [Required]
        [MinLength(RequestConstants.DescriptionMinLength)]
        [MaxLength(RequestConstants.DescriptionMaxLength)]
        public string Description { get; set; }

        public int StatusId { get; set; } = WebConstants.OpenStatusId; // this is the open status id
        public RequestStatus Status { get; set; }

        public int CategoryId { get; set; }
        public RequestCategory Category { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string RequesterId { get; set; }
        public User Requester { get; set; }

        public string AssignedToId { get; set; }
        public User AssignedTo { get; set; }

        public ICollection<RequestAttachment> Attachments { get; set; } = new List<RequestAttachment>();
		
		public ICollection<RequestNote> Notes { get; set; } = new List<RequestNote>();
		public string Resolution {get; set;}
        //public string History {get; set;}

        public ICollection<RequestApproval> Approvals { get; set; } = new List<RequestApproval>();

        public ICollection<RequestReply> Repiles { get; set; } = new List<RequestReply>();
    }
}
