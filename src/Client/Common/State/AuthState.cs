using Blazored.LocalStorage;
using MasterCraft.Client.Common.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MasterCraft.Client.Common.State
{
    public class AuthState : AuthenticationStateProvider
    {
        private readonly HttpClient _httpClient;
        private readonly Microsoft.AspNetCore.Components.Authorization.AuthenticationState _anonymousState;
        private readonly IJSRuntime _js;
        private readonly ILocalStorageService _localStorage;

        public AuthState(HttpClient pHttpClient, IJSRuntime js, ILocalStorageService localStorageService)
        {
            _httpClient = pHttpClient;
            _js = js;
            _anonymousState = new(new ClaimsPrincipal(new ClaimsIdentity()));
            _localStorage = localStorageService;
        }

        public override async Task<Microsoft.AspNetCore.Components.Authorization.AuthenticationState> GetAuthenticationStateAsync()
        {
            string token = (await _localStorage.GetItemAsync<string>("authToken"))?.Replace("\"", "");

            if (string.IsNullOrWhiteSpace(token) || TokenIsExpired(token))
            {
                return _anonymousState;
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            return new Microsoft.AspNetCore.Components.Authorization.AuthenticationState(
                new ClaimsPrincipal(
                    new ClaimsIdentity(JwtParserService.ParseClaimsFromJwt(token), "jwtAuthType")));
        }

        public void NotifyUserAuthentication(string token)
        {
            var authenticatedUser = new ClaimsPrincipal(
                    new ClaimsIdentity(JwtParserService.ParseClaimsFromJwt(token), "jwtAuthType"));

            var authState = Task.FromResult(new Microsoft.AspNetCore.Components.Authorization.AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(_anonymousState);
            NotifyAuthenticationStateChanged(authState);
        }

        private bool TokenIsExpired(string token)
        {
            foreach (Claim claim in JwtParserService.ParseClaimsFromJwt(token))
            {
                if (claim.Type == JwtRegisteredClaimNames.Exp)
                {
                    if (int.TryParse(claim.Value, out int unixSeconds))
                    {
                        DateTime expiry = DateTimeOffset.FromUnixTimeSeconds(unixSeconds).UtcDateTime;
                        if (expiry < DateTime.UtcNow)
                        {
                            NotifyUserLogout();
                            return true;
                        }
                    }
                    else
                    {
                        NotifyUserLogout();
                        return true;
                    }
                }
            }

            return false;
        }

    }
}
