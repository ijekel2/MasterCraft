using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MasterCraft.Infrastructure.Identity;
using MasterCraft.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions;
using Microsoft.EntityFrameworkCore;
using MasterCraft.Application.Common.Interfaces;

namespace MasterCraft.Infrastructure
{
    public static class DependencyInjection
    {
        private const string cDbName = "mastercraft.db";
        private const string cAuthScheme = "JwtBearer";

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //-- EF Core.
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbPath = System.IO.Path.Join(path, cDbName);
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite($"DataSource={dbPath}"));

            //-- ASP.NET Core Identity
            services.AddDefaultIdentity<ExtendedIdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireDigit = false;
                options.User.RequireUniqueEmail = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

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

            services.AddTransient<IIdentityService, IdentityService>();
            services.AddScoped<IDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            return services;
        }
    }
}
