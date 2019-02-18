using BasicDesk.Data.Models.Requests;
using BasicDesk.Services.Interfaces;
using BasicDesk.Services.Repository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BasicDesk.Services
{
    public class ApprovalService : BaseDbService<RequestApproval>, IApprovalService, IDbService<RequestApproval>
    {
        private readonly IRepository<ApprovalStatus> statusRepository;

        public ApprovalService(IRepository<RequestApproval> repository, IRepository<ApprovalStatus> statusRepository) : base(repository)
        {
            this.statusRepository = statusRepository;
        }

        public IQueryable<RequestApproval> GetUserSubmittedApprovals(string userId)
        {
            var approvals = this.GetAll().Where(ap => ap.RequesterId == userId && ap.Status.Name == "Pending");
            return approvals;
        }

        public IQueryable<RequestApproval> GetUserApprovalsToApprove(string userId)
        {
            var approvals = this.GetAll().Where(ap => ap.ApproverId == userId && ap.Status.Name == "Pending");
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
                        await this.SaveChangesAsync();
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
                    await this.SaveChangesAsync();
                }
            }
        }

    }
}
