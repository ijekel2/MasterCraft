using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
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
        private readonly AuthenticationState cAnonymousState;
        readonly IJSRuntime _js;

        public AuthStateProvider(HttpClient pHttpClient, IJSRuntime js)
        {
            cHttpClient = pHttpClient;
            _js = js;
            cAnonymousState = new(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = (await _js.InvokeAsync<string>("localStorage.getItem", "authToken"))?.Replace("\"", "");


            if (string.IsNullOrWhiteSpace(token))
            {
                return cAnonymousState;
            }

            cHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            return new AuthenticationState(
                new ClaimsPrincipal(
                    new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
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
