using Blazored.LocalStorage;
using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.Components;
using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MasterCraft.Client.Authentication
{
    public partial class Login : ComponentBase
    {
        private GenerateTokenViewModel request = new();

        [Inject]
        ApiClient ApiClient { get; set; }

        [Inject]
        AuthenticationStateProvider AuthStateProvider { get; set; }

        [Inject]
        NavigationManager Navigation { get; set; }

        [Inject]
        ILocalStorageService LocalStorage { get; set; }

        private async Task<ApiResponse<AccessTokenViewModel>> OnLoginClick()
        {
            ApiResponse<AccessTokenViewModel> apiResponse =
                await ApiClient.PostAsync<GenerateTokenViewModel, AccessTokenViewModel>("token", request);

            if (apiResponse.Response is not null)
            {
                await LocalStorage.SetItemAsync("authToken", apiResponse.Response.AccessToken);

                (AuthStateProvider as AuthStateProvider).NotifyUserAuthentication(apiResponse.Response.AccessToken);
                Navigation.NavigateTo("/portal");
            }

            return apiResponse;
        }
    }
}
