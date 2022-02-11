using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MasterCraft.Client.Common.Api
{
    public class ApiClient
    {
        readonly IHttpClientFactory cHttpClientFactory;
        readonly ILocalStorageService cLocalStorage;
        readonly AuthenticationStateProvider cAuthenticationStateProvider;
        readonly IConfiguration cConfiguration;

        public ApiClient(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILocalStorageService localStorage, AuthenticationStateProvider authenticationStateProvider)
        {
            cHttpClientFactory = httpClientFactory;
            cLocalStorage = localStorage;
            cAuthenticationStateProvider = authenticationStateProvider;
            cConfiguration = configuration;
        }

        public async Task<ApiResponse<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest requestBody)
        {
            HttpClient client = await SetupHttpClient();
            HttpContent content = new StringContent(JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = await client.PostAsync($"/api/{url.TrimStart('/')}", content);

            return await ParseResponse<TResponse>(response);
        }

        private async Task<ApiResponse<TResponse>> ParseResponse<TResponse>(HttpResponseMessage response)
        {
            string lResponseBody = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new ApiResponse<TResponse>()
                    {
                        Response = JsonSerializer.Deserialize<TResponse>(lResponseBody, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true })
                    };
                case HttpStatusCode.BadRequest:
                    JsonDocument lJson = JsonDocument.Parse(lResponseBody);

                    Dictionary<string, string[]> errors = JsonSerializer.Deserialize<Dictionary<string, string[]>>(lJson.RootElement.GetProperty("errors").ToString());
                    return new ApiResponse<TResponse>()
                    {
                        ErrorDetails = new ValidationProblemDetails(errors)
                        {
                            Title = lJson.RootElement.GetProperty("title").GetString(),
                        }
                    };
                default:
                    throw new HttpRequestException(
                        $"Validation failed. Status Code: {response.StatusCode} ({(int)response.StatusCode})");
            }
        }

        private async Task<HttpClient> SetupHttpClient()
        {
            HttpClient httpClient = cHttpClientFactory.CreateClient("MasterCraft");
            httpClient.BaseAddress = new Uri(cConfiguration["API"].TrimEnd('/'));
            AuthenticationState authState = await cAuthenticationStateProvider.GetAuthenticationStateAsync();

            if (authState.User.Identity?.IsAuthenticated ?? false)
            {
                string lToken = await cLocalStorage.GetItemAsStringAsync("authToken");
                httpClient.DefaultRequestHeaders.Add("Bearer", lToken);
            }
           
            return httpClient;
        }
    }
}
