using MasterCraft.Client.Common.Api;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MasterCraft.Client.Common.Components
{
    public partial class SubmissionForm<TRequest, TResponse> : ComponentBase
    {
        private CustomValidation customValidation = new();
        private Dictionary<string, object> SubmitAttribute = new Dictionary<string, object>()
        {
            {"type","submit" }
        };

        [Parameter]
        public TRequest Request { get; set; }

        [Parameter]
        public Func<Task<ApiResponse<TResponse>>> OnValidSubmit { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string FormTitle { get; set; }

        [Parameter]
        public string ButtonText { get; set; }

        private async Task OnSubmitClick()
        {
            customValidation?.ClearErrors();

            ApiResponse<TResponse> apiResponse = await OnValidSubmit.Invoke();

            if (apiResponse is null)
            {
                customValidation?.DisplayErrors(apiResponse.ErrorDetails);
            }
        }
    }
}
