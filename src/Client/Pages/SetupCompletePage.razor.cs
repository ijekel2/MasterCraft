using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Layouts;
using MasterCraft.Shared.ViewModels.Aggregates;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MasterCraft.Client.Pages
{
    public partial class SetupCompletePage : ComponentBase
    {
        [Inject] public ApiClient Api { get; set; }
        [Inject] public NavigationManager Navigation { get; set; }
        [Parameter] public MentorProfileVm Profile { get; set; } = new();
        [CascadingParameter] public Task<AuthenticationState> AuthState { get; set; }
        [CascadingParameter] public SetupLayout SetupLayout { get; set; }

        protected override async Task OnInitializedAsync()
        {
            string id = (await AuthState).User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var apiResponse = await Api.GetAsync<MentorProfileVm>($"mentors/getProfile?userid={id}");

            if (apiResponse.Success)
            {
                Profile = apiResponse.Response;
            }
        }

        protected override void OnInitialized() => SetupLayout.UpdateProgressTracker(3);

        private void OnCopyLinkClick()
        {
            Navigation.NavigateTo("/portal");
        }

        private void OnGoToAccountClick()
        {
            Navigation.NavigateTo("/portal");
        }

        private string GetLink()
        {
            return Navigation.ToAbsoluteUri($"/go/{Profile.ProfileId}").AbsoluteUri;
        }
    }
}
