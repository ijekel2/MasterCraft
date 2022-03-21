using MasterCraft.Domain.Common.RequestHandling;
using MasterCraft.Domain.Common.Utilities;
using MasterCraft.Shared.Entities;
using MasterCraft.Shared.Requests;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Authentication
{
    public class RegisterUser : RequestHandler<RegisterUserRequest, ApplicationUser>
    {
        public RegisterUser(RequestHandlerService handlerService) : base(handlerService)
        {
        }

        internal override async Task Validate(RegisterUserRequest command, Validator validator, CancellationToken token = new())
        {
            await validator.MustAsync(
                async () => await HandlerService.IdentityService.FindUserByEmailAsync(command.Email) == null,
                Properties.Resources.AccountAlreadyExists);
        }

        internal override async Task<ApplicationUser> Handle(RegisterUserRequest request, CancellationToken token = new())
        {
            ApplicationUser newUser = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Username = request.Email,
                Password = request.Password,

            };

            await HandlerService.IdentityService.CreateUserAsync(newUser);

            return newUser;
        }
    }
}
