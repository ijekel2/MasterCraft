using MasterCraft.Client.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MasterCraft.Client.Layouts
{
    public partial class SetupLayout : LayoutComponentBase
    {
        private ProgressTracker progressTracker;
        private List<ProgressTrackerItem> items;

        [CascadingParameter]
        public Task<AuthenticationState> AuthState { get; set; }

        protected override async Task OnInitializedAsync()
        {
            base.OnInitialized();
            await InitializeProgressTracker();
        }

        public void UpdateProgressTracker(int itemNumber)
        {
            progressTracker.UpdateProgressTracker(itemNumber);
        }

        private async Task InitializeProgressTracker()
        {
            string userId = (await AuthState).User.FindFirst(ClaimTypes.NameIdentifier).Value;
            items = new List<ProgressTrackerItem>()
        {
            new ProgressTrackerItem()
            {
                Label = "Offering",
                OnClick = () => Navigation.NavigateTo("/setup/profile")
            },
            new ProgressTrackerItem()
            {
                Label = "Payments",
                OnClick = async () => await Stripe.RedirectToVendorOnboarding(userId, "setup/profile", "setup/complete")
            },
            new ProgressTrackerItem()
            {
                Label = "Complete",
                OnClick = () => Navigation.NavigateTo("/setup/profile")
            },
        };
        }
    }
}
