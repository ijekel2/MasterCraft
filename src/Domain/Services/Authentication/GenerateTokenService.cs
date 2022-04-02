using MasterCraft.Domain.Services;
using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Shared.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.Authentication
{
    public class GenerateTokenService : DomainService<GenerateTokenViewModel, AccessTokenViewModel>
    {
        public GenerateTokenService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
        }

        internal override async Task Validate(GenerateTokenViewModel request, DomainValidator validator, CancellationToken token = new())
        {
            await validator.MustAsync(
                async () => await Services.IdentityService.IsValidUserNameAndPassword(request.Username, request.Password),
                Properties.Resources.UsernameOrPasswordIncorrect);
        }

        internal override async Task<AccessTokenViewModel> Handle(GenerateTokenViewModel request, CancellationToken token = new())
        {
            return await Services.IdentityService.GenerateToken(request.Username);
        }
    }
}
