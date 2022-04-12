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
    public class DomainServiceDependencies
    {
        internal ILogger<DomainServiceDependencies> Logger { get; }
        internal IIdentityService IdentityService { get; }
        internal ICurrentUserService CurrentUserService { get; }

        public DomainServiceDependencies(ILogger<DomainServiceDependencies> logger, ICurrentUserService currentUserService, IIdentityService identityService)
        {
            Logger = logger;
            IdentityService = identityService;
            CurrentUserService = currentUserService;
        }
    }
}
