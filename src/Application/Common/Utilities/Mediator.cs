using MasterCraft.Application.Common.Exceptions;
using MasterCraft.Application.Common.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace MasterCraft.Application.Common.Utilities
{
    public class Mediator
    {
        private readonly ILogger cLogger;
        private readonly IIdentityService cIdentityService;
        private readonly ICurrentUserService cCurrentUserService;
        private readonly Stopwatch cTimer;

        public Mediator(ILogger<Mediator> logger, ICurrentUserService currentUserService, IIdentityService identityService)
        {
            cLogger = logger;
            cIdentityService = identityService;
            cCurrentUserService = currentUserService;
            cTimer = new Stopwatch();
        }

        public async Task<TResponse> Send<TRequest, TResponse>(TRequest request, IRequestHandler<TRequest, TResponse> handler)
        {
            Validator validator = new();

            try
            {
                string userId = cCurrentUserService.UserId ?? string.Empty;
                string userName = await GetUserName(userId);

                LogRequest(request, userId, userName);

                cTimer.Start();
                
                await handler.Validate(request, validator);

                if (validator.HasFailure)
                {
                    throw new ValidationException(validator.Errors);
                }

                TResponse response = await handler.Handle(request);

                cTimer.Stop();

                LogPerformance(request, cTimer.ElapsedMilliseconds, userId, userName);

                return response;
            }
            catch (Exception ex)
            {
                LogException(request, ex);
                throw;
            }
        }

        private void LogRequest<TRequest>(TRequest request, string userId, string userName)
        {
            var requestName = typeof(TRequest).Name;

            cLogger.LogInformation("MasterCraft Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, userId, userName, request);
        }

        private void LogPerformance<TRequest>(TRequest request, long elapsedMilliseconds, string userId, string userName)
        {
            if (elapsedMilliseconds > 500)
            {
                var requestName = typeof(TRequest).Name;

                cLogger.LogWarning("MasterCraft Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                       requestName, elapsedMilliseconds, userId, userName, request);
            }
        }

        private void LogException<TRequest>(TRequest request, Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            cLogger.LogError(ex, "MasterCraft Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);
        }

        private async Task<string> GetUserName(string userId)
        {  
            string userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                userName = await cIdentityService.GetUserNameAsync(userId);
            }

            return userName;
        }
    }
}
