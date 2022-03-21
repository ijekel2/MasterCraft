using MasterCraft.Domain.Common.Interfaces;
using MasterCraft.Shared.Requests;
using MasterCraft.Shared.Reports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MasterCraft.Domain.Common.Models;
using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Common.RequestHandling;

namespace MasterCraft.Domain.Authentication
{
    public class GenerateToken : RequestHandler<GenerateTokenRequest, AccessTokenReport>
    {
        public GenerateToken(RequestHandlerService handlerService) : base(handlerService)
        {
        }

        internal override async Task Validate(GenerateTokenRequest request, Validator validator, CancellationToken token = new())
        {
            await validator.MustAsync(
                async () => await HandlerService.IdentityService.IsValidUserNameAndPassword(request.Username, request.Password),
                Properties.Resources.UsernameOrPasswordIncorrect);
        }

        internal override async Task<AccessTokenReport> Handle(GenerateTokenRequest request, CancellationToken token = new())
        {
            return await HandlerService.IdentityService.GenerateToken(request.Username);
        }
    }
}
