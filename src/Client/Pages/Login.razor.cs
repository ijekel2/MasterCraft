using Blazored.LocalStorage;
using MasterCraft.Client.Common;
using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.Services;
using MasterCraft.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MasterCraft.Client.Pages
{
    public partial class Login : ComponentBase
    {
        private GenerateTokenVm request = new();

        [Inject] NavigationManager Navigation { get; set; }
        [Inject] ILocalStorageService LocalStorage { get; set; }
        [Inject] AuthenticationService AuthService { get; set; }

        private async Task<ApiResponse<AccessTokenVm>> OnLoginClick()
        {
            var apiResponse = await AuthService.Login(request);

            if (apiResponse.Response is not null)
            {
                string returnUrl = Navigation.QueryString("returnUrl");

                if (!string.IsNullOrEmpty(returnUrl))
                {
                    Navigation.NavigateTo(returnUrl);
                }
                else
                {
                    Navigation.NavigateTo("/portal");
                }
            }

            return apiResponse;
        }
    }
}
