using Blazored.LocalStorage;
using MasterCraft.Shared.Reports;
using MasterCraft.Shared.Requests;
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
using static MasterCraft.Server.IntegrationTests.TestConstants;

namespace MasterCraft.Server.IntegrationTests.Api
{
    public class TestApi
    {
        public static async Task<TestResponse<TResponse>> PostAsync<TRequest, TResponse>(string url, TRequest requestBody)
        {
            HttpContent content = new StringContent(JsonSerializer.Serialize(requestBody), System.Text.Encoding.UTF8, "application/json");
            
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

            var response = await PostAsync<GenerateTokenRequest, AccessTokenReport>("token", request);

            if (response.Success)
            {
                TestBase.Client.DefaultRequestHeaders.Add("Bearer", response.Response.AccessToken);
            }

            return response;
        }

        private static async Task<TestResponse<TResponse>> ParseResponse<TResponse>(HttpResponseMessage response)
        {
            string lResponseBody = await response.Content.ReadAsStringAsync();

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return new TestResponse<TResponse>()
                    {
                        Response = JsonSerializer.Deserialize<TResponse>(lResponseBody, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true })
                    };
                case HttpStatusCode.BadRequest:
                default:
                    JsonDocument lJson = JsonDocument.Parse(lResponseBody);

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
