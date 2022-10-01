using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Notifications;
using System;
using System.Threading.Tasks;

namespace MasterCraft.Client.Components
{
    public partial class ErrorHandler : ComponentBase
    {
        private SfToast errorToast;

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public async Task ProcessError(Exception ex)
        {
            ToastModel model = new ToastModel
            {
                CssClass = "e-toast-danger",
                ShowCloseButton = true,
                Content = $"Error: {ex.Message}"
            };

            await errorToast.ShowAsync(model);
        }
    }
}
