using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.Services;
using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels.Aggregates;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Syncfusion.Blazor.Inputs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MasterCraft.Client.Mentors.Pages
{
    public partial class SetupMentorProfilePage : ComponentBase
    {
        [Inject] public ApiClient ApiClient { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Inject] public StripeService Stripe { get; set; }
        [Inject] public CurrentUserService CurrentUserService { get; set; }
        public MentorProfileVm Profile { get; set; } = new();
        public OfferingVm Offering { get; set; } = new();
        [CascadingParameter] public SetupLayout SetupLayout { get; set; }


        protected override async Task OnInitializedAsync()
        {
            string userId = (await CurrentUserService.GetCurrentUser()).Id;
            var apiResponse = await ApiClient.GetAsync<MentorProfileVm>($"mentors/getProfile?userid={userId}");

            if (apiResponse.Success)
            {
                Profile = apiResponse.Response;

                UserVm lCurrentUser = await CurrentUserService.GetCurrentUser();
                Profile.FirstName = lCurrentUser.FirstName;
                Profile.LastName = lCurrentUser.LastName;
            }

            SetupLayout.UpdateProgressTracker(1);
        }

        private async Task<ApiResponse<MentorCreatedVm>> OnSubmitClick()
        {
            Profile.VideoEmbedUrl = new EmbedCodeService().ParseSourceUrl(Profile.VideoEmbedCode);

            Profile.Offerings = new List<OfferingVm>() { Offering };
            ApiResponse<MentorCreatedVm> apiResponse = await ApiClient.PostFormAsync<MentorProfileVm, MentorCreatedVm>($"mentors/setupProfile", Profile);

            if (apiResponse.Success)
            {
                await Stripe.SetStripeAccountId(apiResponse.Response.UserId, apiResponse.Response.StripeAccountId);
                await Stripe.RedirectToVendorOnboarding(apiResponse.Response.UserId, "setup/profile", "setup/complete");
                await Task.Delay(5000); //-- We are directing away from the app anyway, so just delay hear to make sure the loading screen stays up.
            }

            return apiResponse;
        }
    }
}
