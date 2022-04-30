using MasterCraft.Client.Common.Api;
using MasterCraft.Server.IntegrationTests;
using MasterCraft.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MasterCraft.Client.Mentors.Components
{
    public partial class LandingPageContent : ComponentBase
    {
        [Parameter]
        public MentorVm Profile { get; set; }

        [Parameter]
        public OfferingVm Offering { get; set; }

        [CascadingParameter]
        public Task<AuthenticationState> AuthState { get; set; }

        [Inject]
        public ApiClient Api { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        protected override async Task OnInitializedAsync()
        {
            string userId = (await AuthState).User.FindFirst(ClaimTypes.NameIdentifier).Value;

            //-- make endpoint mentors/id/landingpage
            if (Profile == null)
            {
                ApiResponse<MentorVm> response = await Api.GetAsync<MentorVm>($"mentors/{userId}");

                if (response.Success)
                {
                    Profile = response.Response;
                }
            }

            if (Offering == null)
            {
                ApiResponse<List<OfferingVm>> response = await Api.GetAsync<List<OfferingVm>>($"offerings?mentorid={userId}");

                if (response.Success)
                {
                    //-- For now we will just take one
                    Offering = response.Response[0];
                }
            }
        }

        private void OnGetFeedbackClick()
        {
            Navigation.NavigateTo($"go/{Profile.ProfileCustomUri}/share");
        }

        private string GetTitleLine()
        {
            return $"{Profile.FirstName} {Profile.LastName}, {Profile.PersonalTitle}";
        }

        private MarkupString GetVideoMarkup()
        {
            return (MarkupString)Profile.VideoEmbedCode;
        }

        private string GetPrice()
        {
            return $"${Offering.Price}";
        }

        private string GetDeliveryTime()
        {
            return $"{Offering.DeliveryDays}-day response";
        }
    }
}
