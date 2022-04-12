using MasterCraft.Client.Common.Api;
using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace MasterCraft.Client.Authentication
{
    public partial class Register : ComponentBase
    {
        private RegisterUserVm request = new();

        [Inject]
        ApiClient ApiClient { get; set; }

        [Inject]
        NavigationManager Navigation { get; set; }

        private async Task<ApiResponse<EmptyVm>> OnRegisterClick()
        {
            ApiResponse<EmptyVm> apiResponse =
                await ApiClient.PostAsync<RegisterUserVm, EmptyVm>("register", request);

            if (apiResponse.Response is not null)
            {
                Navigation.NavigateTo("/");
            }

            return apiResponse;
        }
    }
}
