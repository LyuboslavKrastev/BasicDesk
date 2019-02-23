using BasicDesk.Data.Models.Solution;
using BasicDesk.Data.Models.Requests;
using BasicDesk.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BasicDesk.Common.Constants;

namespace BasicDesk.Data
{
    public class BasicDeskDbContext : IdentityDbContext<User>
    {
        public BasicDeskDbContext(DbContextOptions<BasicDeskDbContext> options)
            : base(options){}

        public DbSet<Request> Requests { get; set; }

        public DbSet<RequestReply> RequestReplies { get; set; }
        public DbSet<ReplyAttachment> ReplyAttachments { get; set; }

        public DbSet<RequestStatus> RequestStatuses { get; set; }

        public DbSet<RequestCategory> RequestCategories { get; set; }

        public DbSet<RequestAttachment> RequestAttachments { get; set; }

        public DbSet<RequestApproval> RequestApprovals { get; set; }

        public DbSet<ApprovalStatus> ApprovalStatuses { get; set; }

        public DbSet<RequestNote> RequestNotes { get; set; }

        public DbSet<Solution> Solutions { get; set; }

        public DbSet<SolutionAttachment> SolutionAttachments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Request>()
                .HasOne(u => u.Requester)
                .WithMany(r => r.Requests)
                .HasForeignKey(u => u.RequesterId);

            builder.Entity<Request>()
                .HasMany(r => r.Attachments)
                .WithOne(a => a.Request)
                .HasForeignKey(a => a.RequestId);

            builder.Entity<Solution>()
                .HasMany(s => s.Attachments)
                .WithOne(a => a.Solution)
                .HasForeignKey(a => a.SolutionId);

            //seed approval statuses

            builder.Entity<ApprovalStatus>().HasData(
                new ApprovalStatus { Id = 1, Name = "Pending" },
                new ApprovalStatus { Id = 2, Name = "Approved" },
                new ApprovalStatus { Id = 3, Name = "Denied" }
            );

            builder.Entity<RequestStatus>().HasData(
                new RequestStatus { Id = WebConstants.OpenStatusId, Name = "Open"},
                new RequestStatus { Id = WebConstants.ClosedStatusId, Name = "Closed" },
                new RequestStatus { Id = WebConstants.RejectedStatusId, Name = "Rejected" },
                new RequestStatus { Id = WebConstants.OnHoldStatusId, Name = "On Hold" },
                new RequestStatus { Id = WebConstants.ForApprovalStatusId, Name = "For Approval" }
           );

 

            base.OnModelCreating(builder);
        }
    }
}
