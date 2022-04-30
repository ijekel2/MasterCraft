using MasterCraft.Domain.Services.Authentication;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Infrastructure.Persistence;
using MasterCraft.Domain.Entities;
using MasterCraft.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Net.Http;
using System.Threading.Tasks;
using MasterCraft.Server.IntegrationTests.Helpers;

namespace MasterCraft.Server.IntegrationTests
{
    public class TestBase
    {
        public static WebApplicationFactory<Startup> TestAppFactory { get; private set; } = null!;
        public static HttpClient Client { get; private set; } = null!;
        public IFileStorage FileStorage { get; private set; } = null!;
        public SeedDatabaseHelper SeedHelper => new SeedDatabaseHelper(TestAppFactory.Services.CreateScope());
        public StripeHelper StripeHelper => new StripeHelper(TestAppFactory.Services);

        [OneTimeSetUp]
        public async Task RunBeforeAnyTests()
        {
            TestAppFactory = new WebApplicationFactory();

            //-- Delete DB if its hanging around
            await EnsureDbDeleted();

            //-- Set up DB
            await EnsureDbCreated();

            //-- Set up HttpClient.
            Client = TestAppFactory.CreateClient();
            await CreateTestUsers();

            //-- Get FileStorage service
            FileStorage = TestAppFactory.Services.GetRequiredService<IFileStorage>();
        }

        protected async Task SeedDatabase<TEntity>(params TEntity[] records)
        {
            var dbContextScope = TestAppFactory.Services.CreateScope();
            var context = dbContextScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            foreach (TEntity record in records)
            {
                await context.AddAsync(record);
            }

            await context.SaveChangesAsync();
        }
        
        protected ApplicationDbContext GetDbContext()
        {
            var dbContextScope  = TestAppFactory.Services.CreateScope();
            return dbContextScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        }

        private async Task CreateTestUsers()
        {
            using var scope = TestAppFactory.Services.CreateScope();
            RegisterUserService register = scope.ServiceProvider.GetRequiredService<RegisterUserService>();

            RegisterUserVm request = new()
            {
                FirstName = TestConstants.TestUser.FirstName,
                LastName = TestConstants.TestUser.LastName,
                Email = TestConstants.TestUser.Email,
                Password = TestConstants.TestPassword,
                ConfirmPassword = TestConstants.TestPassword
            };

            await register.HandleRequest(request);
        }

        private async Task EnsureDbCreated()
        {
            var dbContextScope = TestAppFactory.Services.CreateScope();
            using (var context = dbContextScope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
            {
                //-- There will be an 'unable to open database file' exception if VS is set to break on all exceptions
                await context.Database.MigrateAsync();
            }
        }

        public async Task EnsureDbDeleted()
        {
            var dbContextScope = TestAppFactory.Services.CreateScope();
            using (var context = dbContextScope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
            {
                await context.Database.EnsureDeletedAsync();
            }
        }

        [OneTimeTearDown]
        public async Task RunAfterAnyTests()
        {
            await EnsureDbDeleted();
        }
    }
}



