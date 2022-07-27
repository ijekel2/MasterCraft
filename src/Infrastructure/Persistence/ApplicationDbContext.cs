using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MasterCraft.Shared.Enums;

namespace MasterCraft.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<User>, IDbContext
    {
        public DbSet<Mentor> Mentors => Set<Mentor>();

        public DbSet<Offering> Offerings => Set<Offering>();

        public DbSet<FeedbackRequest> FeedbackRequests => Set<FeedbackRequest>();

        public DbSet<Payment> Payments => Set<Payment>();

        public DbSet<Video> Videos => Set<Video>();

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //-- Configure Users table
            modelBuilder.Entity<User>().Property(user => user.Id).HasMaxLength(64);
            modelBuilder.Entity<User>().Property(user => user.FirstName).HasMaxLength(64);
            modelBuilder.Entity<User>().Property(user => user.LastName).HasMaxLength(64);
            modelBuilder.Entity<User>().Property(user => user.ProfileImageUrl).HasMaxLength(1024);
            modelBuilder.Entity<User>().Property(user => user.UserName).HasMaxLength(64);
            modelBuilder.Entity<User>().Property(user => user.NormalizedUserName).HasMaxLength(64);
            modelBuilder.Entity<User>().Property(user => user.Email).HasMaxLength(128);
            modelBuilder.Entity<User>().Property(user => user.NormalizedEmail).HasMaxLength(128);
            modelBuilder.Entity<User>().Property(user => user.PasswordHash).HasMaxLength(1024);
            modelBuilder.Entity<User>().Property(user => user.PhoneNumber).HasMaxLength(64);

            //-- Configure Mentor table
            modelBuilder.Entity<Mentor>().Property(mentor => mentor.UserId).HasMaxLength(64);
            modelBuilder.Entity<Mentor>().Property(mentor => mentor.ProfileId).HasMaxLength(64);
            modelBuilder.Entity<Mentor>().Property(mentor => mentor.StripeAccountId).HasMaxLength(64);
            modelBuilder.Entity<Mentor>().Property(mentor => mentor.SampleQuestion1).HasMaxLength(512);
            modelBuilder.Entity<Mentor>().Property(mentor => mentor.SampleQuestion2).HasMaxLength(512);
            modelBuilder.Entity<Mentor>().Property(mentor => mentor.SampleQuestion3).HasMaxLength(512);
            modelBuilder.Entity<Mentor>().Property(mentor => mentor.SampleQuestion4).HasMaxLength(512);
            modelBuilder.Entity<Mentor>().Property(mentor => mentor.SampleQuestion5).HasMaxLength(512);
            modelBuilder.Entity<Mentor>().HasOne(mentor => mentor.User).WithOne().HasForeignKey<Mentor>(mentor => mentor.UserId);
            modelBuilder.Entity<Mentor>().HasKey(learner => learner.UserId);

            //-- Configure FeedbackRequest table
            modelBuilder.Entity<FeedbackRequest>().Property(request => request.Status)
                .HasConversion(new EnumToStringConverter<FeedbackRequestStatus>())
                .HasMaxLength(32);

            //-- Configure Video table
            modelBuilder.Entity<Video>().Property(video => video.Url).HasMaxLength(1024);
            modelBuilder.Entity<Video>().HasOne(video => video.FeedbackRequest).WithOne().HasForeignKey<Video>(video => video.FeedbackRequestId);
            modelBuilder.Entity<Video>().Property(video => video.VideoType)
                .HasConversion(new EnumToStringConverter<VideoType>())
                .HasMaxLength(32);

            //-- Configure Payment table
            modelBuilder.Entity<Payment>().Property(payment => payment.StripePaymentId).HasMaxLength(64);
            modelBuilder.Entity<Payment>().HasOne(payment => payment.FeedbackRequest).WithOne().HasForeignKey<Payment>(mentor => mentor.FeedbackRequestId);

            base.OnModelCreating(modelBuilder);
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
