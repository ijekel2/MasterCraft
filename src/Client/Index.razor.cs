using MasterCraft.Client.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Threading.Tasks;

namespace MasterCraft.Client
{
    public partial class Index : ComponentBase
    {
        [CascadingParameter] public ErrorHandler Error { get; set; }
        [Inject] NavigationManager Navigation {get; set;}

        private async Task ProcessException()
        {
            await Error.ProcessError(new Exception("This is a handled error."));
        }

        private void ThrowException()
        {
            throw new Exception("This is an unhandled error.");
        }

        private void GoToSignUp(MouseEventArgs args)
        {
            Navigation.NavigateTo("/signup");
        }
    }
}
