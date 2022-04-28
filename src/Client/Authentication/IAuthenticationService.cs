using MasterCraft.Client.Common.Api;
using MasterCraft.Shared.ViewModels;
using System.Threading.Tasks;

namespace MasterCraft.Client.Authentication
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<EmptyVm>> Register(RegisterUserVm registerUserCommand);

        Task<ApiResponse<AccessTokenVm>> Login(GenerateTokenVm pGenerateTokenCommand);

        Task Logout();
    }
}