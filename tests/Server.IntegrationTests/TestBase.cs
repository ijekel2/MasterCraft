using MasterCraft.Domain.Authentication;
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
        public static WebApplicationFactory<Startup> TestAppFactory = null!;
        public static HttpClient Client = null!;

        [OneTimeSetUp]
        public async Task RunBeforeAnyTests()
        {
            TestAppFactory = new WebApplicationFactory();

            await EnsureDbCreated();

            Client = TestAppFactory.CreateClient();
            await CreateTestUsers();
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

        private static async Task EnsureDbCreated()
        {
            using var scope = TestAppFactory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            //-- There will be an 'unable to open database file' exception if VS is set to break on all exceptions
            await context.Database.MigrateAsync();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
        public static async Task EnsureDbDeleted()
        {
            using var scope = TestAppFactory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.EnsureDeletedAsync();
        }

        [OneTimeTearDown]
        public async Task RunAfterAnyTests()
        {
            await EnsureDbDeleted();
        }
    }
}



