using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.StateManagers;
using MasterCraft.Shared.ViewModels;
using System.Threading.Tasks;
using MasterCraft.Client.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace MasterCraft.Client.Common.Services
{
    public class CurrentUserService
    {
        private readonly UserStateManager _userState;
        private readonly AuthStateProvider _authProvider;
        private readonly ApiClient _api;

        public CurrentUserService(UserStateManager userState, AuthenticationStateProvider authProvider, ApiClient api)
        {
            _userState = userState;
            _authProvider = (AuthStateProvider)authProvider;
            _api = api;
        }

        public async Task<UserVm> GetCurrentUser()
        {
            if (_userState.User is null)
            {
                AuthenticationState authState = await _authProvider.GetAuthenticationStateAsync();
                string id = authState.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                ApiResponse<UserVm> apiResponse = await _api.GetAsync<UserVm>($"users/{id}");

                if (apiResponse.Success)
                {
                    _userState.User = apiResponse.Response;
                }
            }

            return _userState.User;
        }
    }
}
