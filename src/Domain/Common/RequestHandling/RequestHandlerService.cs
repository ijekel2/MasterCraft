using MasterCraft.Domain.Common.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Common.RequestHandling
{
    public class RequestHandlerService
    {
        internal ILogger<RequestHandlerService> Logger { get; }
        internal IIdentityService IdentityService { get; }
        internal ICurrentUserService CurrentUserService { get; }

        public RequestHandlerService(ILogger<RequestHandlerService> logger, ICurrentUserService currentUserService, IIdentityService identityService)
        {
            Logger = logger;
            IdentityService = identityService;
            CurrentUserService = currentUserService;
        }
    }
}
