using Blazored.LocalStorage;
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

namespace MasterCraft.Client.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient cHttpClient;
        private readonly AuthStateProvider cAuthStateProvider;
        private readonly ILocalStorageService cLocalStorage;

        public AuthenticationService(HttpClient pHttpClient, AuthenticationStateProvider pAuthStateProvider, ILocalStorageService pLocalStorage)
        {
            cHttpClient = pHttpClient;
            cAuthStateProvider = (AuthStateProvider)pAuthStateProvider;
            cLocalStorage = pLocalStorage;
        }

        public async Task<AccessTokenReportModel> Login(GenerateTokenCommandModel pGenerateTokenCommand)
        {
            var lData = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("username", pGenerateTokenCommand.Username),
                new KeyValuePair<string, string>("password", pGenerateTokenCommand.Password)
            });

            var lAuthResult = await cHttpClient.PostAsync("/api/token", lData);
            var lAuthContent = await lAuthResult.Content.ReadAsStringAsync();

            if (!lAuthResult.IsSuccessStatusCode)
            {
                return null;
            }

            var lResult = JsonSerializer.Deserialize<AccessTokenReportModel>(lAuthContent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            await cLocalStorage.SetItemAsync("authToken", lResult.AccessToken);

            cAuthStateProvider.NotifyUserAuthentication(lResult.AccessToken);
            cHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", lResult.AccessToken);

            return lResult;
        }

        public async Task Logout()
        {
            await cLocalStorage.RemoveItemAsync("authToken");

            cAuthStateProvider.NotifyUserLogout();
            cHttpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
