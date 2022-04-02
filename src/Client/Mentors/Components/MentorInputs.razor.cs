using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.Components;
using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterCraft.Client.Mentors.Components
{
    public partial class MentorInputs : ComponentBase
    {
        [Inject]
        public ApiClient ApiClient { get; set; }

        [Parameter]
        public MentorViewModel Profile { get; set; } = new();

        [Parameter]
        public Action OnValidSubmit { get; set; }

        private async Task<ApiResponse<Empty>> OnSubmit()
        {
            ApiResponse<Empty> apiResponse = await ApiClient.PostAsync<MentorViewModel, Empty>("mentors", Profile);
            apiResponse.Response = new();

            if (apiResponse.Success)
            {
                OnValidSubmit.Invoke();
            }

            return apiResponse;
        }

    }
}
