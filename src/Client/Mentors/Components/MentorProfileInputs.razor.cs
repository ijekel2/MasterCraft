using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.Components;
using MasterCraft.Core.Entities;
using MasterCraft.Core.Reports;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterCraft.Client.Mentors.Components
{
    public partial class MentorProfileInputs : ComponentBase
    {
        [Inject]
        public ApiClient ApiClient { get; set; }

        [Parameter]
        public MentorProfile Profile { get; set; } = new();

        [Parameter]
        public Action OnValidSubmit { get; set; }

        private async Task<ApiResponse<Empty>> OnSubmit()
        {
            //ApiResponse<Empty> apiResponse = await ApiClient.PostAsync<MentorProfile, Empty>("register", Profile);
            ApiResponse<Empty> apiResponse = new();
            apiResponse.Response = new();

            if (apiResponse.Success)
            {
                OnValidSubmit.Invoke();
            }

            return apiResponse;
        }

    }
}
