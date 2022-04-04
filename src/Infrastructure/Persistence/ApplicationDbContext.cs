using MasterCraft.Domain.Interfaces;
using MasterCraft.Infrastructure.Identity;
using MasterCraft.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ExtendedIdentityUser>, IDbContext
    {
        public DbSet<Mentor> Mentors => Set<Mentor>();

        public DbSet<Learner> Learners => Set<Learner>();

        public DbSet<Offering> Offerings => Set<Offering>();

        public DbSet<FeedbackRequest> FeedbackRequests => Set<FeedbackRequest>();

        public DbSet<Payment> Payments => Set<Payment>();

        public DbSet<BankAccount> BankAccounts => Set<BankAccount>();

        public DbSet<PaymentCard> PaymentCards => Set<PaymentCard>();

        public DbSet<Video> Videos => Set<Video>();

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //-- Todo
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedDate = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedDate = DateTime.Now;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
