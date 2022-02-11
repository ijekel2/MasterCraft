using MasterCraft.Client.Common.Api;
using MasterCraft.Core.CommandModels;
using MasterCraft.Core.ReportModels;
using System.Threading.Tasks;

namespace MasterCraft.Client.Authentication
{
    public interface IAuthenticationService
    {
        Task<ApiResponse<Void>> Register(RegisterUserCommandModel registerUserCommand);

        Task<ApiResponse<AccessTokenReportModel>> Login(GenerateTokenCommandModel pGenerateTokenCommand);

        Task Logout();
    }
}