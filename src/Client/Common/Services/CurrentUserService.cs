using MasterCraft.Client.Common.Api;
using MasterCraft.Client.Common.State;
using MasterCraft.Shared.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MasterCraft.Client.Common.Services
{
    public class CurrentUserService
    {
        private readonly UserState _userState;
        private readonly AuthState _authProvider;
        private readonly ApiClient _api;

        public CurrentUserService(UserState userState, AuthenticationStateProvider authProvider, ApiClient api)
        {
            _userState = userState;
            _authProvider = (AuthState)authProvider;
            _api = api;
        }

        public async Task<UserVm> GetCurrentUser()
        {
            if (_userState.User is null)
            {
                Microsoft.AspNetCore.Components.Authorization.AuthenticationState authState = await _authProvider.GetAuthenticationStateAsync();
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
