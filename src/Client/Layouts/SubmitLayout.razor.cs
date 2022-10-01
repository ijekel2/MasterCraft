using MasterCraft.Client.Components;
using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels.Aggregates;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterCraft.Client.Layouts
{
    public partial class SubmitLayout : LayoutComponentBase
    {
        private ProgressTracker progressTracker;
        private List<ProgressTrackerItem> items;

        public MentorProfileVm MentorProfile => SubmissionState.MentorProfile;
        public OfferingVm Offering => SubmissionState.MentorProfile.Offerings.FirstOrDefault();
        public FeedbackRequestVm FeedbackRequest => SubmissionState.FeedbackRequest;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            InitializeProgressTracker();
        }

        public void ReconcilePageBasedOnSubmissionState(string profileId)
        {
            bool hasWork = !string.IsNullOrEmpty(FeedbackRequest.VideoEmbedUrl);
            bool hasQuestion = !string.IsNullOrEmpty(FeedbackRequest.Question);

            if (!hasWork)
            {
                Navigation.NavigateTo($"go/{profileId}/share");
            }

            if (hasWork && !hasQuestion)
            {
                Navigation.NavigateTo($"go/{profileId}/ask");
            }
        }

        public void UpdateProgressTracker(int itemNumber)
        {
            progressTracker.UpdateProgressTracker(itemNumber);
        }

        private string GetTitleLine()
        {
            return $"Submit your work to {MentorProfile.FirstName}";
        }

        private void InitializeProgressTracker()
        {
            items = new List<ProgressTrackerItem>()
            {
                new ProgressTrackerItem()
                {
                    Label = "Share work",
                    OnClick = () => Navigation.NavigateTo($"go/{MentorProfile.ProfileId}/share")
                },
                new ProgressTrackerItem()
                {
                    Label = "Ask question",
                    OnClick = () => Navigation.NavigateTo($"go/{MentorProfile.ProfileId}/ask")
                },
                new ProgressTrackerItem()
                {
                    Label = "Checkout",
                    OnClick = async () => Stripe.RedirectToCheckout(await Stripe.GetCheckoutSession(MentorProfile.StripeAccountId,
                        $"go/{MentorProfile.ProfileId}/ask",
                        $"go/{MentorProfile.ProfileId}/complete",
                        FeedbackRequest,
                        Offering))
                }
            };
        }
    }
}
