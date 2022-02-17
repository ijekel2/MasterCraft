using MasterCraft.Application.Common.Interfaces;
using MasterCraft.Core.CommandModels;
using MasterCraft.Core.Entities;
using MasterCraft.Core.ReportModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Application.Authentication.Commands.RegisterUser
{
    public class RegisterUserCommand : RegisterUserCommandModel, IRequest<ApplicationUser>
    {
        public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, ApplicationUser>
        {
            private readonly IIdentityService cIdentityService;

            public RegisterUserCommandHandler(IIdentityService identityService)
            {
                cIdentityService = identityService;
            }

            public async Task<ApplicationUser> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
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
}
