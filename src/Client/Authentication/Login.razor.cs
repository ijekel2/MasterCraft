using Blazored.LocalStorage;
using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.Components;
using MasterCraft.Core.Requests;
using MasterCraft.Core.Reports;
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
        private GenerateTokenRequest request = new();
        private CustomValidation customValidation;
        private Dictionary<string, object> SubmitAttribute = new Dictionary<string, object>()
        {
            {"type","submit" }
        };

        [Inject]
        ApiClient ApiClient { get; set; }

        [Inject]
        AuthenticationStateProvider AuthStateProvider { get; set; }

        [Inject]
        NavigationManager Navigation { get; set; }

        [Inject]
        ILocalStorageService LocalStorage { get; set; }

        private bool EnableProgress {get; set; }

        private async Task OnLoginClick()
        {
            EnableProgress = true;

            customValidation?.ClearErrors();

            ApiResponse<AccessTokenReport> apiResponse =
                await ApiClient.PostAsync<GenerateTokenRequest, AccessTokenReport>("token", request);

            if (apiResponse.Response is not null)
            {
                await LocalStorage.SetItemAsync("authToken", apiResponse.Response.AccessToken);

                (AuthStateProvider as AuthStateProvider).NotifyUserAuthentication(apiResponse.Response.AccessToken);
                Navigation.NavigateTo("/portal");
            }
            else
            {
                customValidation?.DisplayErrors(apiResponse.ErrorDetails);
            }

            EnableProgress = false;
        }
    }
}
