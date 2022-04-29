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
    public partial class SetupProfilePage : ComponentBase
    {
        [Inject]
        public ApiClient ApiClient { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public StripeService Stripe { get; set; }

        [Parameter]
        public MentorVm Profile { get; set; } = new();

        [CascadingParameter]
        public int CurrentProgressItem { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> AuthState { get; set; }

        protected override void OnInitialized()
        {
            CurrentProgressItem = 1;
        }

        private async Task<ApiResponse<MentorCreatedVm>> OnSubmitClick()
        {
            ApiResponse<MentorCreatedVm> apiResponse = await ApiClient.PostAsync<MentorVm, MentorCreatedVm>("mentors", Profile);
            if (apiResponse.Success)
            {
                string username = (await AuthState).User.FindFirst(ClaimTypes.Name).Value;
                await Stripe.SetStripeAccountId(username, apiResponse.Response.StripeAccountId);
                await Stripe.RedirectToVendorOnboarding(username, "setup/offering");
            }

            return apiResponse;
        }

    }
}
