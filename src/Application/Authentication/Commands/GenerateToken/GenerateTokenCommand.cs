using MasterCraft.Application.Common.Interfaces;
using MasterCraft.Core.CommandModels;
using MasterCraft.Core.ReportModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Application.Authentication.Commands.GenerateToken
{
    public class GenerateTokenCommand : GenerateTokenCommandModel, IRequest<AccessTokenReportModel>
    {
        public class GenerateTokenCommandHandler : IRequestHandler<GenerateTokenCommand, AccessTokenReportModel>
        {
            private readonly IIdentityService cIdentityService;

            public GenerateTokenCommandHandler(IIdentityService identityService)
            {
                cIdentityService = identityService;
            }

            public async Task<AccessTokenReportModel> Handle(GenerateTokenCommand request, CancellationToken cancellationToken)
            {
                 return await cIdentityService.GenerateToken(request.Username);
            }
        }
    }
}
