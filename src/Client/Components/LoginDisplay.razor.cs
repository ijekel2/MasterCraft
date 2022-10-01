using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace MasterCraft.Client.Components
{
    public partial class LoginDisplay : ComponentBase
    {
        [Inject] NavigationManager Navigation { get; set; }

        private void Logout(MouseEventArgs args)
        {
            Navigation.NavigateTo("/logout");
        }

        private void GoToSignUp(MouseEventArgs args)
        {
            Navigation.NavigateTo("/signup");
        }

        private void GoToLogin(MouseEventArgs args)
        {
            Navigation.NavigateTo("/login");
        }

        private void GoToProfile(MouseEventArgs args)
        {
            Navigation.NavigateTo("/profile");
        }
    }
}
