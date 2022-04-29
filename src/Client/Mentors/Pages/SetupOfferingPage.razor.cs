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
        public int CurrentProgressItem { get; set; }

        protected override void OnInitialized()
        {
            CurrentProgressItem = 3;
        }

        private async Task<ApiResponse<EmptyVm>> OnSubmit()
        {
            ApiResponse<EmptyVm> apiResponse = await ApiClient.PostAsync<OfferingVm, EmptyVm>("offerings", Offering);

            if (apiResponse.Success)
            {
                Navigation.NavigateTo("/setup/review");
            }

            return apiResponse;
        }
    }
}
