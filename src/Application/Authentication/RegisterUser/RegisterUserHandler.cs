using MasterCraft.Application.Common.Interfaces;
using MasterCraft.Core.Requests;
using MasterCraft.Core.Entities;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using MasterCraft.Application.Common.Models;
using MasterCraft.Application.Common.Utilities;

namespace MasterCraft.Application.Authentication.RegisterUser
{
    public class RegisterUserHandler : IRequestHandler<RegisterUserRequest, ApplicationUser>
    {
        private readonly IIdentityService cIdentityService;

        public RegisterUserHandler(IIdentityService identityService)
        {
            cIdentityService = identityService;
        }

        public async Task Validate(RegisterUserRequest command, Validator validator)
        {
            await validator.MustAsync(
                async () => await cIdentityService.FindUserByEmailAsync(command.Email) == null,
                Properties.Resources.AccountAlreadyExists);
        }

        public async Task<ApplicationUser> Handle(RegisterUserRequest request)
        {
            ApplicationUser newUser = new()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.Email,
                Password = request.Password,

            };

            await cIdentityService.CreateUserAsync(newUser);

            return newUser;
        }
    }
}
