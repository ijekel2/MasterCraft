using Blazored.LocalStorage;
using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.Services;
using MasterCraft.Client.Common.State;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Blazor;
using System.Threading.Tasks;

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
                .AddSingleton<SubmissionState>()
                .AddSingleton<UserState>()
                .AddScoped<AuthenticationService>()
                .AddScoped<StripeService>()
                .AddScoped<LoomService>()
                .AddTransient<ApiClient>()
                .AddTransient<CurrentUserService>();

            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthState>();
            builder.Services.AddSyncfusionBlazor();

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjI2MTIzQDMyMzAyZTMxMmUzMG5zTUswdFpHek5CNzZCZ00xcld5ekNKVUNzVlVNT1R0SVVrbEhhYnpEa0E9");

            await builder.Build().RunAsync();
        }
    }
}
