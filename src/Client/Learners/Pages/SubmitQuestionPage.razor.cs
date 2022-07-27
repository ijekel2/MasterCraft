using Blazored.LocalStorage;
using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.Enums;
using MasterCraft.Client.Common.Services;
using MasterCraft.Client.Common.StateManagers;
using MasterCraft.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace MasterCraft.Client.Learners.Pages
{
    public partial class SubmitQuestionPage : ComponentBase
    {
        [Inject] public ApiClient ApiClient { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Inject] public ILocalStorageService Storage { get; set; }
        [Inject] public StripeService Stripe { get; set; }
        [Inject] public SubmitStateManager SubmitState { get; set; }
        [Parameter] public string ProfileId { get; set; }
        [CascadingParameter] public SubmitLayout Layout { get; set; }
        public FeedbackRequestVm FeedbackRequest => SubmitState.FeedbackRequest;
        public MentorUserVm MentorUser => SubmitState.MentorProfile.MentorUser;
        public OfferingVm Offering => SubmitState.MentorProfile.Offerings.FirstOrDefault();

        protected override void OnInitialized()
        {
            Layout.UpdateProgressTracker(2);
        }

        private async Task<ApiResponse<FeedbackRequestCreatedVm>> OnSubmitClick()
        {
            ApiResponse<FeedbackRequestCreatedVm> apiResponse = 
                await ApiClient.PostAsync<FeedbackRequestVm, FeedbackRequestCreatedVm>("feedbackrequests", FeedbackRequest);
            
            if (apiResponse.Success)
            {
                FeedbackRequest.Id = apiResponse.Response.FeedbackRequestId;

                await Stripe.RedirectToCheckout(MentorUser.StripeAccountId, 
                    $"go/{MentorUser.ProfileId}/ask", 
                    $"go/{MentorUser.ProfileId}/complete/{FeedbackRequest.Id}",
                    FeedbackRequest,
                    Offering);
            }

               
            return apiResponse;
        }

    }
}

