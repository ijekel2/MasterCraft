using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public async Task<ApiResponse<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest requestBody) where TResponse : new()
        {
            HttpClient client = await SetupHttpClient();
            HttpContent content = new StringContent(JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = await client.PostAsync($"/api/{url.TrimStart('/')}", content);

            return await ParseResponse<TResponse>(response);
        }

        public async Task<ApiResponse<TResponse>> PutAsync<TRequest, TResponse>(string url, TRequest requestBody) where TResponse : new()
        {
            HttpClient client = await SetupHttpClient();
            HttpContent content = new StringContent(JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PutAsync($"/api/{url.TrimStart('/')}", content);

            return await ParseResponse<TResponse>(response);
        }

        public async Task<ApiResponse<TResponse>> GetAsync<TResponse>(string url) where TResponse : new()
        {
            HttpClient client = await SetupHttpClient();

            HttpResponseMessage response = await client.GetAsync($"/api/{url.TrimStart('/')}");

            return await ParseResponse<TResponse>(response);
        }

        private async Task<ApiResponse<TResponse>> ParseResponse<TResponse>(HttpResponseMessage response) where TResponse : new()
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            ApiResponse<TResponse> apiResponse = new();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                    if (!string.IsNullOrEmpty(responseBody))
                    {
                        apiResponse.Response = JsonSerializer.Deserialize<TResponse>(responseBody, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    }
                    else
                    {
                        apiResponse.Response = new TResponse();
                    }
                    return apiResponse;
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.NotFound:
                    JsonDocument lJson = JsonDocument.Parse(responseBody);
                    Dictionary<string, string[]> errors = JsonSerializer.Deserialize<Dictionary<string, string[]>>(lJson.RootElement.GetProperty("errors").ToString());

                    return new ApiResponse<TResponse>()
                    {
                        ErrorDetails = new ValidationProblemDetails(errors)
                        {
                            Title = lJson.RootElement.GetProperty("title").GetString(),
                        }
                    };
                default:
                    return new ApiResponse<TResponse>()
                    {
                        ErrorDetails = JsonSerializer.Deserialize<ProblemDetails>(responseBody, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true })
                    };
            }
        }

        private async Task<HttpClient> SetupHttpClient()
        {
            HttpClient httpClient = cHttpClientFactory.CreateClient("MasterCraft");
            httpClient.BaseAddress = new Uri(cConfiguration["API"].TrimEnd('/'));
            AuthenticationState authState = await cAuthenticationStateProvider.GetAuthenticationStateAsync();

            if (authState.User.Identity?.IsAuthenticated ?? false)
            {
                string token = (await cLocalStorage.GetItemAsStringAsync("authToken")).Replace("\"", "");
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
           
            return httpClient;
        }
    }
}
