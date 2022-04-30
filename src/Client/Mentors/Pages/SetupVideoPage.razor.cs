using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.Services;
using MasterCraft.Client.Shared.Components;
using MasterCraft.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MasterCraft.Client.Mentors.Pages
{
    public partial class SetupVideoPage : ComponentBase
    {
        [Inject]
        public ApiClient ApiClient { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Parameter]
        public UpdateMentorVm Profile { get; set; } = new();

        [CascadingParameter]
        public SetupLayout SetupLayout { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> AuthState { get; set; }

        protected override void OnInitialized()
        {
            SetupLayout.UpdateProgressTracker(4);
        }

        private async Task<ApiResponse<EmptyVm>> OnSubmitClick()
        {
            ApiResponse<EmptyVm> apiResponse = await ApiClient.PutAsync<UpdateMentorVm, EmptyVm>("mentors", Profile);
            if (apiResponse.Success)
            {
                Navigation.NavigateTo("/setup/review");
                
            }

            return apiResponse;
        }

    }
}

