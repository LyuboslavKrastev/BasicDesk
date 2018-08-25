using BasicDesk.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BasicDesk.Data
{
    public class BasicDeskDbContext : IdentityDbContext<User>
    {
        public BasicDeskDbContext(DbContextOptions<BasicDeskDbContext> options)
            : base(options){}

        public DbSet<Request> Requests { get; set; }

        public DbSet<RequestStatus> RequestStatuses { get; set; }

        public DbSet<RequestCategory> RequestCategories { get; set; }

        public DbSet<RequestAttachment> RequestAttachments { get; set; }

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

            base.OnModelCreating(builder);
        }
    }
}
