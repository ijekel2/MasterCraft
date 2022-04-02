using MasterCraft.Client.Common.Api;
using MasterCraft.Shared.ViewModels;
using MasterCraft.Shared.ViewModels;
using System.Threading.Tasks;

namespace MasterCraft.Client.Authentication
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<Empty>> Register(RegisterUserViewModel registerUserCommand);

        Task<ApiResponse<AccessTokenViewModel>> Login(GenerateTokenViewModel pGenerateTokenCommand);

        Task Logout();
    }
}