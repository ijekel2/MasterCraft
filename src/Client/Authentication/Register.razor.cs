using Blazored.LocalStorage;
using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.Components;
using MasterCraft.Core.CommandModels;
using MasterCraft.Core.ReportModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Void = MasterCraft.Core.ReportModels.Void;

namespace MasterCraft.Client.Authentication
{
    public partial class Register : ComponentBase
    {
        private RegisterUserCommandModel command = new();
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

            ApiResponse<Void> apiResponse =
                await ApiClient.PostAsync<RegisterUserCommandModel, Void>("register", command);

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
