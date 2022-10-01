using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.Enums;
using MasterCraft.Client.Common.Models;
using MasterCraft.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace MasterCraft.Client.Components
{
    public partial class Loom : ComponentBase
    {
        DotNetObjectReference<Loom> ObjectReference;
        [Parameter] public Func<LoomVideo, string, Task> OnSubmitFeedbackClick { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            ApiResponse<AccessTokenVm> response = await ApiClient.GetAsync<AccessTokenVm>("loomtoken");

            ObjectReference = DotNetObjectReference.Create(this);
            await JS.InvokeVoidAsync("LoomService.init",
                new object[]
                {
                response?.Response.AccessToken,
                "btn-record",
                ObjectReference,
                "Submit Feedback",
                Colors.McButtonGray,
                Colors.McButtonDarkGray,
                Colors.McButtonDarkGray,
                Colors.McGreen,
                Colors.McDarkGreen,
                Colors.McDarkGreen
                });
        }

        [JSInvokable]
        public async Task OnInsertClick(LoomVideo video, string html)
        {
            await OnSubmitFeedbackClick(video, html);
        }

        public void Dispose()
        {
            ObjectReference?.Dispose();
        }
    }
}
