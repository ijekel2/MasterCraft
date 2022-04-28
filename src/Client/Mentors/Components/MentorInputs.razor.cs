using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.Components;
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
        public MentorVm Profile { get; set; } = new();

        [Parameter]
        public Action OnValidSubmit { get; set; }

        private async Task<ApiResponse<EmptyVm>> OnSubmit()
        {
            ApiResponse<EmptyVm> apiResponse = await ApiClient.PostAsync<MentorVm, EmptyVm>("mentors", Profile);
            apiResponse.Response = new();

            if (apiResponse.Success)
            {
                OnValidSubmit.Invoke();
            }

            return apiResponse;
        }

    }
}
