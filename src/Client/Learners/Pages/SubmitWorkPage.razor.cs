using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.Services;
using MasterCraft.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;

namespace MasterCraft.Client.Learners.Pages
{
    public partial class SubmitWorkPage : ComponentBase
    {
        [Inject]
        public ApiClient ApiClient { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Inject]
        public StripeService Stripe { get; set; }

        [Parameter]
        public FeedbackRequestVm FeedbackRequest { get; set; } = new();

        [Parameter]
        public string ProfileId { get; set; }

        [CascadingParameter]
        public SubmitLayout Layout { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> AuthState { get; set; }

        protected override void OnInitialized()
        {
            Layout.UpdateProgressTracker(1);
        }

        private async Task<ApiResponse<EmptyVm>> OnSubmitClick()
        {
            //ApiResponse<MentorCreatedVm> apiResponse = await ApiClient.PostAsync<MentorVm, MentorCreatedVm>("mentors", Profile);
            //if (apiResponse.Success)
            //{
            //    string username = (await AuthState).User.FindFirst(ClaimTypes.Name).Value;
            //    await Stripe.SetStripeAccountId(username, apiResponse.Response.StripeAccountId);
            //    await Stripe.RedirectToVendorOnboarding(username, "setup/offering");
            //}

            //return apiResponse;

            Navigation.NavigateTo($"go/{Layout.Mentor.ProfileCustomUri}/ask");
            return await Task.FromResult(new ApiResponse<EmptyVm>());
        }

    }
}
