using MasterCraft.Domain.Common.Interfaces;
using MasterCraft.Infrastructure.Identity;
using MasterCraft.Shared.Entities;
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
        public DbSet<Offering> Offerings => Set<Offering>();

        public DbSet<MentorProfile> MentorProfiles => Set<MentorProfile>();

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
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
