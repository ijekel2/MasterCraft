using Blazored.LocalStorage;
using MasterCraft.Shared.Reports;
using MasterCraft.Shared.Requests;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using static MasterCraft.Server.IntegrationTests.TestConstants;

namespace MasterCraft.Server.IntegrationTests.Api
{
    public class TestApi
    {
        public static async Task<TestResponse<TResponse>> GetAsync<TResponse>(string url)
        {
            HttpResponseMessage response = await TestBase.Client.GetAsync($"/api/{url.TrimStart('/')}");

            return await ParseResponse<TResponse>(response);
        }

        public static async Task<TestResponse<TResponse>> PostJsonAsync<TRequest, TResponse>(string url, TRequest requestBody)
        {
            HttpContent content = new StringContent(JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = await TestBase.Client.PostAsync($"/api/{url.TrimStart('/')}", content);

            return await ParseResponse<TResponse>(response);
        }

        public static async Task<TestResponse<TResponse>> PostFormAsync<TRequest, TResponse>(string url, TRequest requestBody, List<string> files)
        {
            MultipartFormDataContent content = new()
            {
                { new StringContent(JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json") }
            };

            foreach (string file in files)
            {
                content.Add(new StreamContent(File.OpenRead(file)), "file", Path.GetFileName(file));
            }

            HttpResponseMessage response = await TestBase.Client.PostAsync($"/api/{url.TrimStart('/')}", content);

            return await ParseResponse<TResponse>(response);
        }

        public static async Task<TestResponse<AccessTokenReport>> AuthenticateMentor()
        {
            GenerateTokenRequest request = new()
            {
                Username = TestMentor.Username,
                Password = TestMentor.Password
            };

            var response = await PostJsonAsync<GenerateTokenRequest, AccessTokenReport>("token", request);

            if (response.Success)
            {
                TestBase.Client.DefaultRequestHeaders.Add("Bearer", response.Response.AccessToken);
            }

            return response;
        }

        private static async Task<TestResponse<TResponse>> ParseResponse<TResponse>(HttpResponseMessage response)
        {
            string responseBody = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    TestResponse<TResponse> testResponse = new();
                    if (!string.IsNullOrEmpty(responseBody))
                    {
                        testResponse.Response = JsonSerializer.Deserialize<TResponse>(responseBody, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    }
                    return testResponse;
                case HttpStatusCode.BadRequest:
                default:
                    JsonDocument lJson = JsonDocument.Parse(responseBody);

                    try
                    {
                        Dictionary<string, string[]> errors = JsonSerializer.Deserialize<Dictionary<string, string[]>>(lJson.RootElement.GetProperty("errors").ToString());
                        
                        return new TestResponse<TResponse>()
                        {
                            ErrorDetails = new ValidationProblemDetails(errors)
                            {
                                Title = lJson.RootElement.GetProperty("title").GetString(),
                            }
                        };
                    }
                    catch
                    {
                        throw new HttpRequestException(
                            $"Validation failed. Status Code: {response.StatusCode} ({(int)response.StatusCode})");
                    }
            }
        }
    }
}
