using BasicDesk.Data.Models.Requests;
using BasicDesk.Services.Repository;
using System.Linq;

namespace BasicDesk.Services
{
    public class ApprovalService
    {
        private readonly DbRepository<RequestApproval> repository;

        public ApprovalService(DbRepository<RequestApproval> repository)
        {
            this.repository = repository;
        }

        public IQueryable<RequestApproval> GetUserSubmittedApprovals(string userId)
        {
            var approvals = this.repository.All().Where(ap => ap.RequesterId == userId);
            return approvals;
        }

        public IQueryable<RequestApproval> GetUserApprovalsToApprove(string userId)
        {
            var approvals = this.repository.All().Where(ap => ap.ApproverId == userId);
            return approvals;
        }
    }
}
