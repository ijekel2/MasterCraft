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

namespace MasterCraft.Client.Learners.Pages
{
    public partial class SubmitQuestionPage : ComponentBase
    {
        [Inject] public ApiClient ApiClient { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Inject] public StripeService Stripe { get; set; }
        [Parameter] public FeedbackRequestVm FeedbackRequest { get; set; } = new();
        [Parameter] public string ProfileId { get; set; }
        [CascadingParameter] public SubmitLayout Layout { get; set; }
        [CascadingParameter] public Task<AuthenticationState> AuthState { get; set; }

        protected override void OnInitialized()
        {
            Layout.UpdateProgressTracker(2);
        }

        private async Task<ApiResponse<EmptyVm>> OnSubmitClick()
        {
            //ApiResponse<EmptyVm> apiResponse = await ApiClient.PutAsync<UpdateMentorVm, EmptyVm>("mentors", Profile);
            //if (apiResponse.Success)
            //{
            //    Navigation.NavigateTo("/setup/review");

            //}

            //return apiResponse;

            await Stripe.RedirectToCheckout(Layout.Mentor.StripeAccountId, Layout.Offering.Price, $"go/{Layout.Mentor.ProfileCustomUri}/ask", $"go/{Layout.Mentor.ProfileCustomUri}/complete");
            return await Task.FromResult(new ApiResponse<EmptyVm>());
        }

    }
}

