using FluentValidation;
using MasterCraft.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Application.Authentication.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        private readonly IIdentityService cIdentityService;

        public RegisterUserCommandValidator(IIdentityService identityService)
        {
            cIdentityService = identityService;

            RuleFor(v => v)
                .MustAsync(NotMatchExistingUser).WithMessage(Properties.Resources.AccountAlreadyExists);
        }

        private async Task<bool> NotMatchExistingUser(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            return await cIdentityService.FindUserByEmailAsync(command.Email) == null;
        }
    }
}
