using Blazored.LocalStorage;
using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.Services;
using MasterCraft.Client.Layouts;
using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels.Aggregates;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MasterCraft.Client.Pages
{
    public partial class SubmitCompletePage : ComponentBase
    {
        [Inject] public ApiClient Api { get; set; }
        [Inject] public ILocalStorageService Storage { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Inject] public CurrentUserService UserService { get; set; }
        [Parameter] public MentorProfileVm Profile { get; set; } = new();
        [Parameter] public string ProfileId { get; set; }
        [Parameter] public string FeedbackRequestId { get; set; }
        [CascadingParameter] public SubmitLayout SubmitLayout { get; set; }
        private UserVm cCurrentUser = new();

        protected override async Task OnInitializedAsync()
        {
            cCurrentUser = await UserService.GetCurrentUser();

            var submitResponse = await Api.PostAsync<SubmitFeedbackRequestVm, EmptyVm>($"feedbackrequests/submit",
                new SubmitFeedbackRequestVm() { FeedbackRequestId = FeedbackRequestId });

            if (submitResponse.Success)
            {
                var apiResponse = await Api.GetAsync<MentorProfileVm>($"mentors/getProfile?profileid={ProfileId}");

                if (apiResponse.Success)
                {
                    Profile = apiResponse.Response;
                }
            }
            else
            {
                Navigation.NavigateTo("/notfound");
            }

        }

        private void OnCopyLinkClick()
        {
            Navigation.NavigateTo("/portal");
        }

        private void OnGoToAccountClick()
        {
            Navigation.NavigateTo("/portal");
        }

        private string GetConfirmationPhrase()
        {
            return $"Your work was sent to {Profile.FirstName} {Profile.LastName}.";
        }

        private string GetStepOneText()
        {
            return $"Check your email. We sent confirmation to {cCurrentUser.Email}";
        }

        private string GetStepTwoText()
        {
            return $"{Profile.FirstName} will look at your video and record a personal with feedback — usually within 24-72 hrs.";
        }

        private string GetStepThreeText()
        {
            return $"When your feedback is completed, we'll email you a link to view or download your feedback video.";
        }

        private string GetRefundText()
        {
            return $"If {Profile.FirstName} isn't able to complete your request, the hold on your card will be removed within 5-7 business days.";
        }
    }
}
