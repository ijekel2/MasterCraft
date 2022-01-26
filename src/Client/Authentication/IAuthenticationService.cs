using MasterCraft.Core.CommandModels;
using MasterCraft.Core.ReportModels;
using System.Threading.Tasks;

namespace MasterCraft.Client.Authentication
{
    public interface IAuthenticationService
    {
        Task<AccessTokenReportModel> Login(GenerateTokenCommandModel pGenerateTokenCommand);
        Task Logout();

        Task Register(RegisterUserCommandModel registerUserCommand);
    }
}