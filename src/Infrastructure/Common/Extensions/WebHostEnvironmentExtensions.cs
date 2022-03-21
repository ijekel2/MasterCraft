using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Infrastructure.Common.Extensions
{
    public static class WebHostEnvironmentExtensions
    {
        public static bool IsTest(this IWebHostEnvironment webHostEnvironment)
        {
            return EqualsTest(webHostEnvironment.EnvironmentName);
        }

        public static bool IsTest(this IHostEnvironment hostEnvironment)
        {
            return EqualsTest(hostEnvironment.EnvironmentName);
        }

        private static bool EqualsTest(string environment)
        {
            return environment.ToLower().Trim().Equals("Test");
        }
    }
}
