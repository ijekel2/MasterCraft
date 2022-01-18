using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MasterCraft.Application.Common.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MasterCraft.Application.Common.Behaviors
{
    public class PerformanceBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly Stopwatch cTimer;
        private readonly ILogger<TRequest> cLogger;
        private readonly ICurrentUserService cCurrentUserService;
        private readonly IIdentityService cIdentityService;

        public PerformanceBehavior(
            ILogger<TRequest> logger,
            ICurrentUserService currentUserService,
            IIdentityService identityService)
        {
            cTimer = new Stopwatch();

            cLogger = logger;
            cCurrentUserService = currentUserService;
            cIdentityService = identityService;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            cTimer.Start();

            var response = await next();

            cTimer.Stop();

            var elapsedMilliseconds = cTimer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500)
            {
                var requestName = typeof(TRequest).Name;
                var userId = cCurrentUserService.UserId ?? string.Empty;
                var userName = string.Empty;

                if (!string.IsNullOrEmpty(userId))
                {
                    userName = await cIdentityService.GetUserNameAsync(userId);
                }

                cLogger.LogWarning("MasterCraft Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                    requestName, elapsedMilliseconds, userId, userName, request);
            }

            return response;
        }
    }
}


