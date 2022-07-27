using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.Services;
using MasterCraft.Client.Common.StateManagers;
using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels.Aggregates;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace MasterCraft.Client.Learners.Pages
{
    public partial class SubmitWorkPage : ComponentBase
    {
        [Inject] public ApiClient ApiClient { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Inject] public StripeService Stripe { get; set; }
        [Inject] public SubmitStateManager SubmitState { get; set; }
        [Inject] public CurrentUserService CurrentUser { get; set; }
        [Parameter] public string ProfileId { get; set; }
        [CascadingParameter] public SubmitLayout Layout { get; set; }

        public FeedbackRequestVm FeedbackRequest => SubmitState.FeedbackRequest;

        protected override async Task OnInitializedAsync()
        {
            if (SubmitState.MentorProfile.MentorUser.UserId is null)
            {
                var apiResponse = await ApiClient.GetAsync<MentorProfileVm>($"mentors/getProfile?profileid={ProfileId}");

                if (apiResponse.Success)
                {
                    SubmitState.MentorProfile = apiResponse.Response;
                }
            }

            FeedbackRequest.MentorId = SubmitState.MentorProfile.MentorUser.UserId;
            FeedbackRequest.LearnerId = (await CurrentUser.GetCurrentUser()).Id;
            FeedbackRequest.OfferingId = SubmitState.MentorProfile.Offerings.FirstOrDefault()?.Id ?? 0;

            Layout.UpdateProgressTracker(1);
        }

        private async Task<ApiResponse<EmptyVm>> OnSubmitClick()
        {
            FeedbackRequest.VideoEmbedUrl = new EmbedCodeService().ParseSourceUrl(FeedbackRequest.VideoEmbedCode);
            Navigation.NavigateTo($"go/{Layout.MentorUser.ProfileId}/ask");
            return await Task.FromResult(new ApiResponse<EmptyVm>());
        }

    }
}
