using MasterCraft.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace MasterCraft.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            var hostEnv = host.Services.GetService<IHostEnvironment>();
            
            //-- Auto migrate DB on startup
            if (hostEnv.IsDevelopment() || hostEnv.IsStaging())
            {
                using (var scope = host.Services.CreateScope())
                {
                    try
                    {
                        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                        db.Database.Migrate();
                    }
                    catch
                    {
                        //-- We did our best
                    }
                }
            }
           
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }

        private static bool IsStaging()
        {
            return (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? string.Empty).ToLower().Trim().Equals("staging");
        }
    }
}
