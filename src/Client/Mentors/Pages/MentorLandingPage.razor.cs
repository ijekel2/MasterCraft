using MasterCraft.Client.Common;
using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.State;
using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels.Aggregates;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterCraft.Client.Mentors.Pages
{
    public partial class MentorLandingPage : ComponentBase
    {
        [Parameter] public string ProfileId { get; set; }
        [Parameter] public MentorProfileVm Profile { get; set; } = new();
        [Inject] public ApiClient Api { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Inject] public SubmissionState SubmitState { get; set; }

        private MentorUserVm MentorUser => Profile.MentorUser;
        private OfferingVm Offering => Profile.Offerings.FirstOrDefault() ?? new();

        protected override async Task OnInitializedAsync()
        {
            var apiResponse = await Api.GetAsync<MentorProfileVm>($"mentors/getProfile?profileid={ProfileId}");

            if (apiResponse.Success)
            {
                Profile = apiResponse.Response;
            }
        }

        private void OnGetFeedbackClick()
        {
            SubmitState.MentorProfile = Profile;
            Navigation.NavigateTo($"go/{Profile.MentorUser.ProfileId}/share");
        }

        private string GetTitleLine()
        {
            return $"{Profile.MentorUser.FirstName} {Profile.MentorUser.LastName}";
        }

        private string GetPrice()
        {
            return $"{Offering.Price.FormatPrice()}";
        }

    }
}
