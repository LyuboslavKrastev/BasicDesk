using System;
using System.Collections.Generic;
using System.Text;

namespace BasicDesk.App.Models.Common.ViewModels
{
    public class RequestApprovalViewModel
    {
        public int Id { get; set; }

        public string ApproverId { get; set; }

        public string RequestId { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public string Status { get; set; }
    }
}
