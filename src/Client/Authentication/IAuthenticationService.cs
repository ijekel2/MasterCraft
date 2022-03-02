using MasterCraft.Client.Common.Api;
using MasterCraft.Core.Requests;
using MasterCraft.Core.Reports;
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