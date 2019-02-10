using BasicDesk.App.Models.Management.ViewModels;
using BasicDesk.Data.Models.Requests;
using System.Collections.Generic;

namespace BasicDesk.App.Models.Common.ViewModels
{
    public class IndexViewModel
    {
        public ICollection<ReportViewModel> Reports { get; set; }

        public IEnumerable<RequestApproval> SubmittedApprovals { get; set; }

        public IEnumerable<RequestApproval> ApprovalsToApprove { get; set; }
    }
}
