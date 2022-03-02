using Blazored.LocalStorage;
using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.Components;
using MasterCraft.Core.Requests;
using MasterCraft.Core.Reports;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Empty = MasterCraft.Core.Reports.Empty;

namespace MasterCraft.Client.Authentication
{
    public partial class Register : ComponentBase
    {
        private RegisterUserRequest command = new();
        private CustomValidation customValidation;
        private Dictionary<string, object> SubmitAttribute = new Dictionary<string, object>()
        {
            {"type","submit" }
        };

        [Inject]
        ApiClient ApiClient { get; set; }

        [Inject]
        NavigationManager Navigation { get; set; }

        private async Task OnRegisterClick()
        {
            customValidation?.ClearErrors();

            ApiResponse<Empty> apiResponse =
                await ApiClient.PostAsync<RegisterUserRequest, Empty>("register", command);

            if (apiResponse.Response is not null)
            {
                Navigation.NavigateTo("/");
            }
            else
            {
                customValidation?.DisplayErrors(apiResponse.ErrorDetails);
            }
        }
    }
}
