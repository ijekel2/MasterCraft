using MasterCraft.Domain.Interfaces;
using MasterCraft.Infrastructure.Identity;
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
            //-- Configure Users table
            modelBuilder.Entity<ExtendedIdentityUser>().Property(mentor => mentor.Id).HasMaxLength(64);
            modelBuilder.Entity<ExtendedIdentityUser>().Property(mentor => mentor.FirstName).HasMaxLength(64);
            modelBuilder.Entity<ExtendedIdentityUser>().Property(mentor => mentor.LastName).HasMaxLength(64);
            modelBuilder.Entity<ExtendedIdentityUser>().Property(mentor => mentor.UserName).HasMaxLength(64);
            modelBuilder.Entity<ExtendedIdentityUser>().Property(mentor => mentor.NormalizedUserName).HasMaxLength(64);
            modelBuilder.Entity<ExtendedIdentityUser>().Property(mentor => mentor.Email).HasMaxLength(128);
            modelBuilder.Entity<ExtendedIdentityUser>().Property(mentor => mentor.NormalizedEmail).HasMaxLength(128);
            modelBuilder.Entity<ExtendedIdentityUser>().Property(mentor => mentor.PasswordHash).HasMaxLength(64);
            modelBuilder.Entity<ExtendedIdentityUser>().Property(mentor => mentor.PhoneNumber).HasMaxLength(64);

            //-- Configure Mentor table
            modelBuilder.Entity<Mentor>().Property(mentor => mentor.ApplicationUserId).HasMaxLength(64);
            modelBuilder.Entity<Mentor>().Property(mentor => mentor.ChannelLink).HasMaxLength(256);
            modelBuilder.Entity<Mentor>().Property(mentor => mentor.ChannelName).HasMaxLength(64);
            modelBuilder.Entity<Mentor>().Property(mentor => mentor.PersonalTitle).HasMaxLength(64);
            modelBuilder.Entity<Mentor>().Property(mentor => mentor.ProfileCustomUri).HasMaxLength(64);
            modelBuilder.Entity<Mentor>().Property(mentor => mentor.ProfileImageUrl).HasMaxLength(256);
            modelBuilder.Entity<Mentor>().Property(mentor => mentor.ProfileImageUrl).HasMaxLength(256);


            //-- Configure Learner table
            modelBuilder.Entity<Learner>().Property(learner => learner.ProfileImageUrl).HasMaxLength(256);
            modelBuilder.Entity<Learner>().Property(learner => learner.ApplicationUserId).HasMaxLength(64);

            //-- Configure Offering table
            modelBuilder.Entity<Offering>().Property(offering => offering.Name).HasMaxLength(64);
            modelBuilder.Entity<Offering>().Property(offering => offering.Description).HasMaxLength(256);
            modelBuilder.Entity<Offering>().Property(offering => offering.SampleQuestion1).HasMaxLength(256);
            modelBuilder.Entity<Offering>().Property(offering => offering.SampleQuestion2).HasMaxLength(256);
            modelBuilder.Entity<Offering>().Property(offering => offering.SampleQuestion3).HasMaxLength(256);
            modelBuilder.Entity<Offering>().Property(offering => offering.SampleQuestion4).HasMaxLength(256);
            modelBuilder.Entity<Offering>().Property(offering => offering.SampleQuestion5).HasMaxLength(256);

            //-- Configure FeedbackRequest table
            modelBuilder.Entity<FeedbackRequest>().Property(request => request.ContentLink).HasMaxLength(256);
            modelBuilder.Entity<FeedbackRequest>().Property(request => request.Status)
                .HasConversion(new EnumToStringConverter<FeedbackRequestStatus>())
                .HasMaxLength(32);

            //-- Configure Video table
            modelBuilder.Entity<Video>().Property(video => video.Uri).HasMaxLength(256);
            modelBuilder.Entity<Video>().Property(video => video.VideoType)
                .HasConversion(new EnumToStringConverter<VideoType>())
                .HasMaxLength(32);

            //-- Configure Payment table
            modelBuilder.Entity<Payment>().Property(payment => payment.AuthorizationCode).HasMaxLength(64);
            modelBuilder.Entity<Payment>().Property(payment => payment.TransactionId).HasMaxLength(64);

            //-- Configure BankAccount table
            modelBuilder.Entity<BankAccount>().Property(account => account.Institution).HasMaxLength(64);
            modelBuilder.Entity<BankAccount>().Property(account => account.AccountNumber).HasMaxLength(64);
            modelBuilder.Entity<BankAccount>().Property(account => account.RoutingNumber).HasMaxLength(64);
            modelBuilder.Entity<BankAccount>().Property(account => account.AccountType)
                .HasConversion(new EnumToStringConverter<AccountType>())
                .HasMaxLength(32);

            //-- Configure PaymentCard table
            modelBuilder.Entity<PaymentCard>().Property(card => card.CardNumber).HasMaxLength(64);
            modelBuilder.Entity<PaymentCard>().Property(card => card.BillingFirstName).HasMaxLength(64);
            modelBuilder.Entity<PaymentCard>().Property(card => card.BillingLastName).HasMaxLength(64);
            modelBuilder.Entity<PaymentCard>().Property(card => card.BillingCompany).HasMaxLength(64);
            modelBuilder.Entity<PaymentCard>().Property(card => card.BillingStreet).HasMaxLength(128); 
            modelBuilder.Entity<PaymentCard>().Property(card => card.BillingPremise).HasMaxLength(64);
            modelBuilder.Entity<PaymentCard>().Property(card => card.BillingCity).HasMaxLength(64);
            modelBuilder.Entity<PaymentCard>().Property(card => card.BillingState).HasMaxLength(64);
            modelBuilder.Entity<PaymentCard>().Property(card => card.BillingPostalCode).HasMaxLength(64);
            modelBuilder.Entity<PaymentCard>().Property(card => card.BillingCountry).HasMaxLength(64);

            //-- Billing Address: TODO

            modelBuilder.Entity<PaymentCard>().Property(account => account.CardType)
               .HasConversion(new EnumToStringConverter<PaymentCardType>())
               .HasMaxLength(32);
            modelBuilder.Entity<PaymentCard>().Property(account => account.CardNetwork)
               .HasConversion(new EnumToStringConverter<PaymentCardNetwork>())
               .HasMaxLength(32);

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
