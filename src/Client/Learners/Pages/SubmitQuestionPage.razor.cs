using Blazored.LocalStorage;
using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.Services;
using MasterCraft.Client.Common.State;
using MasterCraft.Client.Shared.Components;
using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels.Aggregates;
using Microsoft.AspNetCore.Components;
using System;
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
        [Inject] public SubmissionState SubmitState { get; set; }
        [Parameter] public string ProfileId { get; set; }
        [CascadingParameter] public SubmitLayout Layout { get; set; }
        [CascadingParameter] ErrorHandler ErrorHandler { get; set; }

        public FeedbackRequestVm FeedbackRequest => SubmitState.FeedbackRequest;
        public MentorProfileVm MentorProfile => SubmitState.MentorProfile;
        public OfferingVm Offering => SubmitState.MentorProfile.Offerings.FirstOrDefault();

        protected override void OnInitialized()
        {
            Layout.ReconcilePageBasedOnSubmissionState(ProfileId);
            Layout.UpdateProgressTracker(2);
        }

        private async Task<ApiResponse<FeedbackRequestCreatedVm>> OnSubmitClick()
        {
            try
            {
                CheckoutSessionVm checkoutSession = await Stripe.GetCheckoutSession(MentorProfile.StripeAccountId,
                    $"go/{MentorProfile.ProfileId}/ask",
                    $"go/{MentorProfile.ProfileId}/complete/{FeedbackRequest.Id}",
                    FeedbackRequest,
                    Offering);

                FeedbackRequest.PaymentIntentId = checkoutSession.PaymentIntentId;

                ApiResponse<FeedbackRequestCreatedVm> apiResponse =
                    await ApiClient.PostAsync<FeedbackRequestVm, FeedbackRequestCreatedVm>("feedbackrequests", FeedbackRequest);

                if (apiResponse.Success)
                {
                    FeedbackRequest.Id = apiResponse.Response.FeedbackRequestId;

                    Stripe.RedirectToCheckout(checkoutSession);
                }

                return apiResponse;
            }
            catch (Exception ex)
            {
                ErrorHandler?.ProcessError(ex);
            }

            return new();
            
        }

    }
}

