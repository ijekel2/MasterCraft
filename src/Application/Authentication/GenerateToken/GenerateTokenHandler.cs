using MasterCraft.Application.Common.Interfaces;
using MasterCraft.Core.Requests;
using MasterCraft.Core.Reports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MasterCraft.Application.Common.Models;
using MasterCraft.Application.Common.Utilities;

namespace MasterCraft.Application.Authentication.GenerateToken
{
    public class GenerateTokenHandler : IRequestHandler<GenerateTokenRequest, AccessTokenReport>
    {
        private readonly IIdentityService cIdentityService;

        public GenerateTokenHandler(IIdentityService identityService)
        {
            cIdentityService = identityService;
        }

        public virtual async Task Validate(GenerateTokenRequest command, Validator validator)
        {
            await validator.MustAsync(
                async () => await cIdentityService.IsValidUserNameAndPassword(command.Username, command.Password),
                Properties.Resources.UsernameOrPasswordIncorrect);
        }

        public virtual async Task<AccessTokenReport> Handle(GenerateTokenRequest request)
        {
            return await cIdentityService.GenerateToken(request.Username);
        }
    }
}
