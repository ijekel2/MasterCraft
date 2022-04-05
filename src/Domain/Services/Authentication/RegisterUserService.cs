using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Domain.Models;
using MasterCraft.Shared.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services.Authentication
{
    public class RegisterUserService : DomainService<RegisterUserViewModel, ApplicationUser>
    {
        public RegisterUserService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
        }

        internal override async Task Validate(RegisterUserViewModel command, DomainValidator validator, CancellationToken token = new())
        {
            await validator.MustAsync(
                async () => await Services.IdentityService.FindUserByEmailAsync(command.Email) == null,
                Properties.Resources.AccountAlreadyExists);
        }

        internal override async Task<ApplicationUser> Handle(RegisterUserViewModel request, CancellationToken token = new())
        {
            ApplicationUser newUser = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Username = request.Email,
                Password = request.Password,

            };

            await Services.IdentityService.CreateUserAsync(newUser);

            return newUser;
        }
    }
}
