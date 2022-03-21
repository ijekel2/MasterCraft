using Blazored.LocalStorage;
using MasterCraft.Client.Common.Api;
using MasterCraft.Shared.Requests;
using MasterCraft.Shared.Reports;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace MasterCraft.Client.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ApiClient cApiClient;
        private readonly AuthStateProvider cAuthStateProvider;
        private readonly ILocalStorageService cLocalStorage;

        public AuthenticationService(ApiClient apiClient, AuthenticationStateProvider pAuthStateProvider, ILocalStorageService pLocalStorage)
        {
            cAuthStateProvider = (AuthStateProvider)pAuthStateProvider;
            cLocalStorage = pLocalStorage;
            cApiClient = apiClient;
        }

        public async Task<ApiResponse<AccessTokenReport>> Login(GenerateTokenRequest generateTokenCommand)
        {
            ApiResponse<AccessTokenReport> apiResponse = 
                await cApiClient.PostAsync<GenerateTokenRequest, AccessTokenReport>("token", generateTokenCommand);
            
            if (apiResponse.Response is not null)
            {
                await cLocalStorage.SetItemAsync("authToken", apiResponse.Response.AccessToken);
                cAuthStateProvider.NotifyUserAuthentication(apiResponse.Response.AccessToken);
            }
            return apiResponse;
        }

        public async Task Logout()
        {
            await cLocalStorage.RemoveItemAsync("authToken");
            cAuthStateProvider.NotifyUserLogout();
        }

        public async Task<ApiResponse<Empty>> Register(RegisterUserRequest registerUserCommand)
        {
            return await cApiClient.PostAsync<RegisterUserRequest, Empty>("token", registerUserCommand);
        }
    }
}
