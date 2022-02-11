using Blazored.LocalStorage;
using MasterCraft.Client.Common.Api;
using MasterCraft.Core.CommandModels;
using MasterCraft.Core.ReportModels;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using Void = MasterCraft.Core.ReportModels.Void;

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

        public async Task<ApiResponse<AccessTokenReportModel>> Login(GenerateTokenCommandModel generateTokenCommand)
        {
            ApiResponse<AccessTokenReportModel> apiResponse = 
                await cApiClient.PostAsync<GenerateTokenCommandModel, AccessTokenReportModel>("token", generateTokenCommand);
            
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

        public async Task<ApiResponse<Void>> Register(RegisterUserCommandModel registerUserCommand)
        {
            return await cApiClient.PostAsync<RegisterUserCommandModel, Void>("token", registerUserCommand);
        }
    }
}
