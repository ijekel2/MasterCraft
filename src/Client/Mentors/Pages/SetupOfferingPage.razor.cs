using MasterCraft.Client.Common.Api;
using MasterCraft.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterCraft.Client.Mentors.Pages
{
    public partial class SetupOfferingPage : ComponentBase
    {
        [Inject]
        public ApiClient ApiClient { get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Parameter]
        public OfferingVm Offering { get; set; } = new();

        [CascadingParameter]
        public SetupLayout SetupLayout { get; set; }

        protected override void OnInitialized()
        {
            SetupLayout.UpdateProgressTracker(3);
        }

        private async Task<ApiResponse<EmptyVm>> OnSubmitClick()
        {
            ApiResponse<EmptyVm> apiResponse = await ApiClient.PostAsync<OfferingVm, EmptyVm>("offerings", Offering);

            if (apiResponse.Success)
            {
                Navigation.NavigateTo("/setup/video");
            }

            return apiResponse;
        }
    }
}
