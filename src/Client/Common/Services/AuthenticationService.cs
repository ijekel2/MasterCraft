using Blazored.LocalStorage;
using MasterCraft.Client.Authentication;
using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.State;
using MasterCraft.Shared.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace MasterCraft.Client.Common.Services
{
    public class AuthenticationService
    {
        private readonly ApiClient _apiClient;
        private readonly AuthStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly UserStateManager _userStateManager;

        public AuthenticationService(ApiClient apiClient, AuthenticationStateProvider authStateProvider, 
            ILocalStorageService localStorage, UserStateManager userStateManager)
        {
            _authStateProvider = (AuthStateProvider)authStateProvider;
            _localStorage = localStorage;
            _apiClient = apiClient;
            _userStateManager = userStateManager;
        }

        public async Task<ApiResponse<AccessTokenVm>> Login(GenerateTokenVm generateTokenCommand)
        {
            ApiResponse<AccessTokenVm> apiResponse = 
                await _apiClient.PostAsync<GenerateTokenVm, AccessTokenVm>("token", generateTokenCommand);
            
            if (apiResponse.Response is not null)
            {
                await _localStorage.SetItemAsync("authToken", apiResponse.Response.AccessToken);
                _authStateProvider.NotifyUserAuthentication(apiResponse.Response.AccessToken);
                _userStateManager.User = apiResponse.Response.User;
            }
            return apiResponse;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            _authStateProvider.NotifyUserLogout();
            _userStateManager.User = new();
        }

        public async Task<ApiResponse<EmptyVm>> Register(RegisterUserVm registerUserCommand)
        {
            return await _apiClient.PostAsync<RegisterUserVm, EmptyVm>("token", registerUserCommand);
        }

        public async Task<string> GetAuthToken()
        {
            return (await _localStorage.GetItemAsStringAsync("authToken"))?.Replace("\"", "") ?? String.Empty;
        }
    }
}
