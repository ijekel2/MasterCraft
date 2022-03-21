﻿using MasterCraft.Client.Common.Api;
using MasterCraft.Shared.Entities;
using MasterCraft.Shared.Reports;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterCraft.Client.Mentors.Components
{
    public partial class OfferingInputs : ComponentBase
    {
        [Inject]
        public ApiClient ApiClient { get; set; }

        [Parameter]
        public Offering Offering { get; set; } = new();

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
