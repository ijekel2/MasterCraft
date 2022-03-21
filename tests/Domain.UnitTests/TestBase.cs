using MasterCraft.Domain.Authentication;
using MasterCraft.Domain.Common.Interfaces;
using MasterCraft.Domain.Common.RequestHandling;
using MasterCraft.Infrastructure.Identity;
using MasterCraft.Infrastructure.Persistence;
using MasterCraft.Shared.Entities;
using MasterCraft.Shared.Reports;
using MasterCraft.Shared.Requests;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Domain.UnitTests
{
    public class TestBase
    {
        protected Mock<ILogger<RequestHandlerService>> cLogger = null!;
        protected Mock<ICurrentUserService> cCurrentUserService = null!;
        protected Mock<IIdentityService> cIdentityService = null!;
        protected RequestHandlerService cHandlerService = null!;
        protected ApplicationDbContext cDbContext = null!;

        [SetUp]
        public void BaseSetup()
        {
            cLogger = new();
            cCurrentUserService = new();
            cIdentityService = new();

            //-- Setup SQLite In-Memory DbContext.
            var options = CreateOptions<ApplicationDbContext>();
            cDbContext = new ApplicationDbContext(options);
            cDbContext.Database.EnsureCreated();

            //-- Mock current user service.
            cCurrentUserService.Setup(service => service.UserId).Returns(() => Guid.NewGuid().ToString());

            //-- Create RequestHandlerService to use for RequestHandler instances
            cHandlerService = new RequestHandlerService(cLogger.Object, cCurrentUserService.Object, cIdentityService.Object);
        }

        [TearDown]
        public void BaseTearDown()
        {
            cDbContext.Dispose();
        }

        protected async Task SeedDatabase<T>(params T[] records)
        {
            foreach (T record in records)
            {
                await cDbContext.AddAsync(record);
            }

            await cDbContext.SaveChangesAsync();
        }

        protected void AuthenticateUser(ApplicationUser user)
        {
            cIdentityService.Setup(service => service.FindUserByEmailAsync(It.IsAny<string>())).Returns(() => Task.FromResult(user));
            cIdentityService.Setup(service => service.CreateUserAsync(It.IsAny<ApplicationUser>())).Returns(() => Task.CompletedTask);
            cIdentityService.Setup(service => service.IsValidUserNameAndPassword(It.IsAny<string>(), It.IsAny<string>())).Returns(() => Task.FromResult(true));
            cIdentityService.Setup(service => service.GenerateToken(It.IsAny<string>())).Returns(() => Task.FromResult(new AccessTokenReport()));
            cIdentityService.Setup(service => service.GetUserNameAsync(It.IsAny<string>())).Returns(() => Task.FromResult(user.Username));
        }

        //-- https://www.thereformedprogrammer.net/using-in-memory-databases-for-unit-testing-ef-core-applications/
        private DbContextOptions CreateOptions<T>()
                where T : DbContext
        {
            //This creates the SQLite connection string to in-memory database
            var connectionStringBuilder = new SqliteConnectionStringBuilder
            { DataSource = ":memory:" };

            var connectionString = connectionStringBuilder.ToString();

            //This creates a SqliteConnectionwith that string
            var connection = new SqliteConnection(connectionString);

            //The connection MUST be opened here
            connection.Open();

            //Now we have the EF Core commands to create SQLite options
            var builder = new DbContextOptionsBuilder<T>();
            builder.UseSqlite(connection);

            return builder.Options;
        }
    }
}
