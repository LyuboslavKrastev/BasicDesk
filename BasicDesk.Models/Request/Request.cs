using BasicDesk.Common.Constants.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BasicDesk.Models.Requests
{
    public class Request
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

        public int StatusId { get; set; }
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
		
		//new additions
		public ICollection<RequestNote> Notes { get; set; } = new List<RequestNote>(); // note userid content
		public string Resolution {get; set;}
		//public string History {get; set;}
    }
}
