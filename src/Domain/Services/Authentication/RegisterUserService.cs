using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Entities;
using MasterCraft.Domain.Models;
using MasterCraft.Shared.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.Authentication
{
    public class RegisterUserService : DomainService<RegisterUserVm, UserVm>
    {
        public RegisterUserService(DomainServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
        }

        internal override async Task Validate(RegisterUserVm command, DomainValidator validator, CancellationToken token = new())
        {
            await validator.MustAsync(
                async () => await Services.IdentityService.FindUserByEmailAsync(command.Email) == null,
                Properties.Resources.AccountAlreadyExists);
        }

        internal override async Task<UserVm> Handle(RegisterUserVm request, CancellationToken token = new())
        {
            await Services.IdentityService.CreateUserAsync(request);

            return new UserVm()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Username = request.Email,
                ProfileImageUrl = request.ProfileImageUrl
            };
        }
    }
}
