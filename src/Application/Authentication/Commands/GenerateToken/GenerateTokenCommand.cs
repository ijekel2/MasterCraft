using MasterCraft.Application.Common.Interfaces;
using MasterCraft.Core.CommandModels;
using MasterCraft.Core.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Application.Authentication.Commands.GenerateToken
{
    public class GenerateTokenCommand : GenerateTokenRequest, IRequest<AuthenticatedUserDto>
    {
        public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommand, AuthenticatedUserDto>
        {
            private readonly IDbContext cDbContext;
            private readonly IIdentityService cIdentityService;

            public GenerateTokenCommandHandler(IDbContext dbContext)
            {
                cDbContext = dbContext;
            }

            public async Task<AuthenticatedUserDto> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
            {
                 return await cIdentityService.GenerateToken(request.Username);
            }
        }
    }
}
