using MasterCraft.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Services
{
    public class ServiceDependencies
    {
        internal ILogger<ServiceDependencies> Logger { get; }
        internal IIdentityService IdentityService { get; }
        internal ICurrentUserService CurrentUserService { get; }

        public ServiceDependencies(ILogger<ServiceDependencies> logger, ICurrentUserService currentUserService, IIdentityService identityService)
        {
            Logger = logger;
            IdentityService = identityService;
            CurrentUserService = currentUserService;
        }
    }
}
