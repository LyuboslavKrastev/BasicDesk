using BasicDesk.Data.Models.Requests;
using BasicDesk.Services.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.Services
{
    public class ApprovalService
    {
        private readonly DbRepository<RequestApproval> repository;
        private readonly DbRepository<ApprovalStatus> statusRepository;


        public ApprovalService(DbRepository<RequestApproval> repository, DbRepository<ApprovalStatus> statusRepository)
        {
            this.repository = repository;
            this.statusRepository = statusRepository;
        }

        public IQueryable<RequestApproval> GetUserSubmittedApprovals(string userId)
        {
            var approvals = this.repository.All().Where(ap => ap.RequesterId == userId && ap.Status.Name == "Pending");
            return approvals;
        }

        public IQueryable<RequestApproval> GetUserApprovalsToApprove(string userId)
        {
            var approvals = this.repository.All().Where(ap => ap.ApproverId == userId && ap.Status.Name == "Pending");
            return approvals;
        }

        public async Task ApproveApproval(int approvalId, string userId)
        {
            RequestApproval approval = await this.repository.All().FirstOrDefaultAsync(ra => ra.Id == approvalId && ra.Status.Name == "Pending");

            if(approval != null)
            {
                if (approval.ApproverId == userId)
                {
                    ApprovalStatus status = await this.statusRepository.All().FirstOrDefaultAsync(s => s.Name == "Approved");
                    if (status != null)
                    {
                        approval.StatusId = status.Id;
                        await this.repository.SaveChangesAsync();
                    }
                }
            }      
        }


        public async Task DenyApproval(int approvalId, string userId)
        {
            RequestApproval approval = await this.repository.All().FirstOrDefaultAsync(ra => ra.Id == approvalId);

            if (approval.ApproverId == userId)
            {
                ApprovalStatus status = await this.statusRepository.All().FirstOrDefaultAsync(s => s.Name == "Denied");
                if (status != null)
                {
                    approval.StatusId = status.Id;
                    await this.repository.SaveChangesAsync();
                }
            }
        }

    }
}
