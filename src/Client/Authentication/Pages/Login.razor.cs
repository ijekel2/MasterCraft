using Blazored.LocalStorage;
using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.Services;
using MasterCraft.Client.Shared.Components;
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

namespace MasterCraft.Client.Authentication.Pages
{
    public partial class Login : ComponentBase
    {
        private GenerateTokenVm request = new();

        [Inject] ApiClient ApiClient { get; set; }
        [Inject] AuthenticationStateProvider AuthStateProvider { get; set; }
        [Inject] NavigationManager Navigation { get; set; }
        [Inject] ILocalStorageService LocalStorage { get; set; }
        [Inject] AuthenticationService AuthService { get; set; }

        private async Task<ApiResponse<AccessTokenVm>> OnLoginClick()
        {
            var apiResponse = await AuthService.Login(request);

            if (apiResponse.Response is not null)
            {
                Navigation.NavigateTo("/portal");
            }

            return apiResponse;
        }
    }
}
