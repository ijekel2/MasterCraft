using MasterCraft.Domain.Authentication;
using MasterCraft.Domain.Common.Interfaces;
using MasterCraft.Infrastructure.Persistence;
using MasterCraft.Shared.Entities;
using MasterCraft.Shared.Requests;
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
        public static WebApplicationFactory<Startup> TestAppFactory = null!;
        public static HttpClient Client = null!;
        public ApplicationDbContext AppDbContext = null!;
        public IFileStorage FileStorage = null!;

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
            RegisterUser register = scope.ServiceProvider.GetRequiredService<RegisterUser>();

            RegisterUserRequest request = new()
            {
                FirstName = TestConstants.TestMentor.FirstName,
                LastName = TestConstants.TestMentor.LastName,
                Email = TestConstants.TestMentor.Email,
                Password = TestConstants.TestMentor.Password,
                ConfirmPassword = TestConstants.TestMentor.Password
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
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



