using MasterCraft.Client.Common.Api;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MasterCraft.Client.Shared.Components
{
    public partial class SubmissionForm<TRequest, TResponse> : ComponentBase
    {
        private bool cShowValidationSummary = false;
        private bool showSpinner = false;
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
            showSpinner = true;

            try
            {
                customValidation?.ClearErrors();

                ApiResponse<TResponse> apiResponse = await OnValidSubmit.Invoke();

                if (!apiResponse.Success)
                {
                    customValidation?.DisplayErrors(apiResponse.ErrorDetails);
                    cShowValidationSummary = true;
                }
            }
            finally
            {
                showSpinner = false;
                StateHasChanged();
            }
        }
    }
}
