using MasterCraft.Server;
using MasterCraft.Application.Common.Interfaces;
using MasterCraft.Core.Entities;
using MasterCraft.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using Respawn;

namespace MasterCraft.Application.Integration
{
    [SetUpFixture]
    public class Testing
    {
        private static IConfigurationRoot cConfiguration = null!;
        private static IServiceScopeFactory cScopeFactory = null!;
        private static IServiceProvider cServiceProvider = null!;
        private static string cCurrentUserId;

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            cConfiguration = builder.Build();

            var startup = new Startup(cConfiguration);

            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "MasterCraft.Server"));

            services.AddLogging();

            startup.ConfigureServices(services);

            // Replace service registration for ICurrentUserService
            // Remove existing registration
            var currentUserServiceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(ICurrentUserService));

            if (currentUserServiceDescriptor != null)
            {
                services.Remove(currentUserServiceDescriptor);
            }

            // Register testing version
            services.AddTransient(provider =>
                Mock.Of<ICurrentUserService>(s => s.UserId == cCurrentUserId));

            cServiceProvider = services.BuildServiceProvider();
            cScopeFactory = cServiceProvider.GetRequiredService<IServiceScopeFactory>();
        }

        private static async Task EnsureDatabase()
        {
            using var scope = cScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            await context.Database.MigrateAsync();
        }

        public static TService GetService<TService>()
        {
            return cServiceProvider.GetRequiredService<TService>();
        }

        public static async Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, IRequestHandler<TRequest, TResponse> handler)
        {
            var mediator = cServiceProvider.GetRequiredService<Common.Utilities.Mediator>();

            return await mediator.Send(request, handler);
        }

        public static async Task<string> RunAsDefaultUserAsync()
        {
            return await RunAsUserAsync("test@local", "Testing1234!", Array.Empty<string>());
        }

        public static async Task<string> RunAsAdministratorAsync()
        {
            return await RunAsUserAsync("mentor@local", "Mentor1234!", new[] { "Mentor" });
        }

        public static async Task<string> RunAsUserAsync(string userName, string password, string[] roles)
        {
            using var scope = cScopeFactory.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var user = new ApplicationUser { UserName = userName, Email = userName };

            var result = await userManager.CreateAsync(user, password);

            if (roles.Any())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                foreach (var role in roles)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                await userManager.AddToRolesAsync(user, roles);
            }

            if (result.Succeeded)
            {
                cCurrentUserId = user.Id;

                return cCurrentUserId;
            }

            var errors = string.Join(Environment.NewLine, result.Errors);

            throw new Exception($"Unable to create {userName}.{Environment.NewLine}{errors}");
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "EF1001:Internal EF Core API usage.", Justification = "<Pending>")]
        public static async Task TearDown()
        {
            using var scope = cScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.EnsureDeletedAsync();
            context.GetDependencies().StateManager.ResetState();
            cCurrentUserId = null;
        }

        public static async Task Setup()
        {
            await EnsureDatabase();
        }

        public static async Task<TEntity> FindAsync<TEntity>(params object[] keyValues)
            where TEntity : class
        {
            using var scope = cScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            return await context.FindAsync<TEntity>(keyValues);
        }

        public static async Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            using var scope = cScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            context.Add(entity);

            await context.SaveChangesAsync();
        }

        public static async Task<int> CountAsync<TEntity>() where TEntity : class
        {
            using var scope = cScopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            return await context.Set<TEntity>().CountAsync();
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
        }
    }
}

