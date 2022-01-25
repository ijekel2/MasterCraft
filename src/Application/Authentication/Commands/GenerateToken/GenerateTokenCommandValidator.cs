using FluentValidation;
using MasterCraft.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Application.Authentication.Commands.GenerateToken
{
    public class GenerateTokenCommandValidator : AbstractValidator<GenerateTokenCommand>
    {
        private readonly IIdentityService cIdentityService;

        public GenerateTokenCommandValidator(IIdentityService identityService)
        {
            cIdentityService = identityService;

            RuleFor(v => v)
                .MustAsync(BeValidUserNameAndPassword).WithMessage(Properties.Resources.UsernameOrPasswordIncorrect);
        }

        private async Task<bool> BeValidUserNameAndPassword(GenerateTokenCommand command, CancellationToken cancellationToken)
        {
            return await cIdentityService.IsValidUserNameAndPassword(command.Username, command.Password);
        }
    }
}
