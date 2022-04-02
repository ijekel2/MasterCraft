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

namespace MasterCraft.Server.IntegrationTests
{
    public class TestBase
    {
        private IServiceScope cDbContextScope = null!;

        public static WebApplicationFactory<Startup> TestAppFactory { get; private set; } = null!;
        public static HttpClient Client { get; private set; } = null!;
        public ApplicationDbContext AppDbContext { get; private set; } = null!;
        public IFileStorage FileStorage { get; private set; } = null!;

        [OneTimeSetUp]
        public async Task RunBeforeAnyTests()
        {
            TestAppFactory = new WebApplicationFactory();

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
            var context = cDbContextScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            foreach (TEntity record in records)
            {
                await context.AddAsync(record);
            }

            await context.SaveChangesAsync();
        }

        private async Task CreateTestUsers()
        {
            using var scope = TestAppFactory.Services.CreateScope();
            RegisterUserService register = scope.ServiceProvider.GetRequiredService<RegisterUserService>();

            RegisterUserViewModel request = new()
            {
                FirstName = TestConstants.TestUser.FirstName,
                LastName = TestConstants.TestUser.LastName,
                Email = TestConstants.TestUser.Email,
                Password = TestConstants.TestUser.Password,
                ConfirmPassword = TestConstants.TestUser.Password
            };

            await register.HandleRequest(request);
        }

        private async Task EnsureDbCreated()
        {
            cDbContextScope = TestAppFactory.Services.CreateScope();
            var context = cDbContextScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            //-- There will be an 'unable to open database file' exception if VS is set to break on all exceptions
            await context.Database.MigrateAsync();

            AppDbContext = context;
        }

        public async Task EnsureDbDeleted()
        {
            await AppDbContext.Database.EnsureDeletedAsync();
            cDbContextScope.Dispose();
        }

        [OneTimeTearDown]
        public async Task RunAfterAnyTests()
        {
            await EnsureDbDeleted();
        }
    }
}



