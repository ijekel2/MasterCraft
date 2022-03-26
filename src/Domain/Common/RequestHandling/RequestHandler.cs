using AutoMapper;
using MasterCraft.Domain.Common.Exceptions;
using MasterCraft.Domain.Common.Interfaces;
using MasterCraft.Domain.Common.Utilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MasterCraft.Domain.Common.RequestHandling
{
    public abstract class RequestHandler<TRequest, TResponse>
    {
        private readonly Stopwatch cTimer;

        internal RequestHandlerService HandlerService { get; }

        public RequestHandler(RequestHandlerService requestHandlerService)
        {
            HandlerService = requestHandlerService;
            cTimer = new();
        }

        public async Task<TResponse> HandleRequest(TRequest request, CancellationToken token = new())
        {
            Validator validator = new();

            try
            {
                string userId = HandlerService.CurrentUserService.UserId ?? string.Empty;
                string userName = await GetUserName(userId);

                LogRequest(request, userId, userName);

                cTimer.Start();

                await Validate(request, validator, token);

                if (validator.HasFailure)
                {
                    throw new ValidationException(validator.Errors);
                }

                TResponse response = await Handle(request, token);

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

        internal abstract Task Validate(TRequest request, Validator validator, CancellationToken pToken = new());

        internal abstract Task<TResponse> Handle(TRequest request, CancellationToken pToken = new());

        protected TDestination Map<TSource, TDestination>(TSource source)
        {
            var mapperConfig = new MapperConfiguration(config => config.CreateMap<TSource, TDestination>());
            TDestination destination = mapperConfig.CreateMapper().Map<TDestination>(source);
            return destination;
        }

        private void LogRequest(TRequest request, string userId, string userName)
        {
            var requestName = typeof(TRequest).Name;

            HandlerService.Logger.LogInformation("MasterCraft Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, userId, userName, request);
        }

        private void LogPerformance(TRequest request, long elapsedMilliseconds, string userId, string userName)
        {
            if (elapsedMilliseconds > 500)
            {
                var requestName = typeof(TRequest).Name;

                HandlerService.Logger.LogWarning("MasterCraft Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                       requestName, elapsedMilliseconds, userId, userName, request);
            }
        }

        private void LogException(TRequest request, Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            HandlerService.Logger.LogError(ex, "MasterCraft Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);
        }

        private async Task<string> GetUserName(string userId)
        {
            string userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                userName = await HandlerService.IdentityService.GetUserNameAsync(userId);
            }

            return userName;
        }
    }
}
