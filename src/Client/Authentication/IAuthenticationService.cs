using MasterCraft.Client.Common.Api;
using MasterCraft.Shared.Requests;
using MasterCraft.Shared.Reports;
using System.Threading.Tasks;

namespace MasterCraft.Client.Authentication
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<Empty>> Register(RegisterUserRequest registerUserCommand);

        Task<ApiResponse<AccessTokenReport>> Login(GenerateTokenRequest pGenerateTokenCommand);

        Task Logout();
    }
}