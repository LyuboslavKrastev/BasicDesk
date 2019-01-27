using BasicDesk.Common.Constants;
using BasicDesk.Common.Constants.Validation;
using System.ComponentModel.DataAnnotations;

namespace BasicDesk.Data.Models.Requests
{
    public class RequestApproval
    {
        public int Id { get; set; }

        [MinLength(RequestConstants.SubjectMinLength)]
        [MaxLength(RequestConstants.SubjectMaxLength)]
        public string Subject { get; set; }

        public string Description { get; set; }

        public int RequestId { get; set; }
        public Request Request { get; set; }

        public string RequesterId { get; set; }
        public User Requester { get; set; }

        public string ApproverId { get; set; }
        public User Approver { get; set; }

        public int StatusId { get; set; }
        public ApprovalStatus Status { get; set; }

        public string ApproverComment { get; set; }
    }
}
