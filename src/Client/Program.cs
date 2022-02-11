using Blazored.LocalStorage;
using MasterCraft.Client.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MasterCraft.Client;
using MasterCraft.Client.Common.Api;

namespace MasterCraft.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            
            builder.Services.AddHttpClient("MasterCraft");

            builder.Services
                .AddScoped<IAuthenticationService, AuthenticationService>()
                .AddTransient<ApiClient>();

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            builder.Services.AddSyncfusionBlazor(options => { options.IgnoreScriptIsolation = true; });

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTcyMzc1QDMxMzkyZTM0MmUzMG1nNi9ldkdHWWw3d3RTQ3lWbDJJN0FDTFY1eXNQOVBFaGxQRVFwa2E4Y289");

            await builder.Build().RunAsync();
        }
    }
}
