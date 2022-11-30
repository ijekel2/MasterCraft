using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Infrastructure.FileStorage;
using MasterCraft.Infrastructure.Identity;
using MasterCraft.Infrastructure.Payments;
using MasterCraft.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.AzureAppServices;
using Microsoft.IdentityModel.Tokens;
using Stripe;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace MasterCraft.Infrastructure
{
    public static class DependencyInjection
    {
        private const string cAuthScheme = "JwtBearer";

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //-- EF Core.
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration["ConnectionString"]));


            //-- ASP.NET Core Identity
            services.AddDefaultIdentity<User>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            //-- Azure logging
            services.Configure<AzureFileLoggerOptions>(options =>
            {
                options.FileName = "azure-diagnostics-";
                options.FileSizeLimit = 50 * 1024;
                options.RetainedFileCountLimit = 5;
            });

            //-- Jwt Auth
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = cAuthScheme;
                options.DefaultChallengeScheme = cAuthScheme;
            })
            .AddJwtBearer(cAuthScheme, jwtOptions =>
            {
                jwtOptions.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySaltIsTheBestSalt")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(5)
                };
            });

            //-- Configure Stripe
            StripeConfiguration.ApiKey = configuration["STRIPE_PRIVATE_KEY"];

            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<IFileStorage, LocalFileStorage>();
            services.AddTransient<IPaymentService, StripePaymentService>();
            services.AddScoped<IDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            return services;
        }
    }
}
