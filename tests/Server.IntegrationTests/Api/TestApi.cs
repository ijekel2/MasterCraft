using MasterCraft.Shared.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
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
            where TResponse : new()
        {
            HttpResponseMessage response = await TestBase.Client.GetAsync($"/api/{url.TrimStart('/')}");

            return await ParseResponse<TResponse>(response);
        }

        public static async Task<TestResponse<TResponse>> PostJsonAsync<TRequest, TResponse>(string url, TRequest requestBody) 
            where TResponse : new()
        {
            HttpContent content = new StringContent(JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json");
            
            HttpResponseMessage response = await TestBase.Client.PostAsync($"/api/{url.TrimStart('/')}", content);

            return await ParseResponse<TResponse>(response);
        }

        public static async Task<TestResponse<TResponse>> PostFormAsync<TRequest, TResponse>(string url, TRequest requestBody, List<string> files) 
            where TResponse : new()
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

        public static async Task<TestResponse<AccessTokenViewModel>> AuthenticateMentor()
        {
            GenerateTokenViewModel request = new()
            {
                Username = TestUser.Username,
                Password = TestUser.Password
            };

            var response = await PostJsonAsync<GenerateTokenViewModel, AccessTokenViewModel>("token", request);

            if (response.Success)
            {
                TestBase.Client.DefaultRequestHeaders.Add("Bearer", response.Response.AccessToken);
            }

            return response;
        }

        private static async Task<TestResponse<TResponse>> ParseResponse<TResponse>(HttpResponseMessage response) where TResponse : new()
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            TestResponse<TResponse> testResponse = new();
            testResponse.StatusCode = response.StatusCode;

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                case HttpStatusCode.Created:
                    if (!string.IsNullOrEmpty(responseBody))
                    {
                        testResponse.Response = JsonSerializer.Deserialize<TResponse>(responseBody, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    }
                    else
                    {
                        testResponse.Response = new TResponse();
                    }

                    testResponse.Headers.Location = response.Headers.Location?.AbsolutePath;
                    testResponse.Success = true;
                    return testResponse;
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.NotFound:
                default:
                    if (!string.IsNullOrEmpty(responseBody))
                    {
                        JsonDocument lJson = JsonDocument.Parse(responseBody);

                        try
                        {
                            Dictionary<string, string[]> errors = JsonSerializer.Deserialize<Dictionary<string, string[]>>(lJson.RootElement.GetProperty("errors").ToString());

                            testResponse.ErrorDetails = new ValidationProblemDetails(errors)
                            {
                                Title = lJson.RootElement.GetProperty("title").GetString(),
                            };

                            testResponse.Success = false;
                        }
                        catch
                        {
                            throw new HttpRequestException(
                                $"Validation failed. Status Code: {response.StatusCode} ({(int)response.StatusCode})");
                        }
                    }

                    return testResponse;
                    
            }
        }
    }
}
