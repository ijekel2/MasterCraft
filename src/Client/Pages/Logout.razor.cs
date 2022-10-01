using MasterCraft.Client.Common.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MasterCraft.Client.Pages
{
    public partial class Logout : ComponentBase
    {
        [Inject] NavigationManager Navigation { get; set; }
        [Inject] AuthenticationService AuthService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await AuthService.Logout();
            Navigation.NavigateTo("/");
        }
    }
}
