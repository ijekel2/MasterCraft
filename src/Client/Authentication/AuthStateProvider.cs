using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MasterCraft.Client.Authentication
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient cHttpClient;
        private readonly ILocalStorageService cLocalStorage;
        private readonly AuthenticationState cAnonymousState;

        public AuthStateProvider(HttpClient pHttpClient, ILocalStorageService pLocalStorage)
        {
            cHttpClient = pHttpClient;
            cLocalStorage = pLocalStorage;
            cAnonymousState = new(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var lToken = await cLocalStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(lToken))
            {
                return cAnonymousState;
            }

            cHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", lToken);

            return new AuthenticationState(
                new ClaimsPrincipal(
                    new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(lToken), "jwtAuthType")));
        }

        public void NotifyUserAuthentication(string pToken)
        {
            var lAuthenticatedUser = new ClaimsPrincipal(
                    new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(pToken), "jwtAuthType"));

            var lAuthState = Task.FromResult(new AuthenticationState(lAuthenticatedUser));
            NotifyAuthenticationStateChanged(lAuthState);
        }

        public void NotifyUserLogout()
        {
            var lAuthState = Task.FromResult(cAnonymousState);
            NotifyAuthenticationStateChanged(lAuthState);
        }
    }
}
