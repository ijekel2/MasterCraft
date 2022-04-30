using AutoMapper;
using MasterCraft.Domain.Exceptions;
using MasterCraft.Domain.Interfaces;
using MasterCraft.Domain.Common.Utilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MasterCraft.Domain.Parameters;
using Microsoft.EntityFrameworkCore;

namespace MasterCraft.Domain.Services
{
    public abstract class DomainService<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;

        public DomainServiceDependencies Services { get; }

        public DomainService(DomainServiceDependencies serviceDependencies)
        {
            Services = serviceDependencies;
            _timer = new();
        }

        public async Task<TResponse> HandleRequest(TRequest request, CancellationToken token = new())
        {
            DomainValidator validator = new();

            try
            {
                string userId = Services.CurrentUserService.UserId ?? string.Empty;
                string userName = await GetUserName(userId);

                LogRequest(request, userId, userName);

                _timer.Start();

                await Validate(request, validator, token);

                if (validator.HasFailure)
                {
                    throw new ValidationException(validator.Errors);
                }

                TResponse response = await Handle(request, token);

                _timer.Stop();

                LogPerformance(request, _timer.ElapsedMilliseconds, userId, userName);

                return response;
            }
            catch (Exception ex)
            {
                LogException(request, ex);
                throw;
            }
        }

        internal abstract Task Validate(TRequest request, DomainValidator validator, CancellationToken token = new());

        internal abstract Task<TResponse> Handle(TRequest request, CancellationToken token = new());

        protected TDestination Map<TSource, TDestination>(TSource source)
        {
            var mapperConfig = new MapperConfiguration(config => config.CreateMap<TSource, TDestination>());
            TDestination destination = mapperConfig.CreateMapper().Map<TDestination>(source);
            return destination;
        }

        protected TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
        {
            var mapperConfig = new MapperConfiguration(config => config.CreateMap<TSource, TDestination>());
            mapperConfig.CreateMapper().Map(source, destination);
            return destination;
        }

        protected async Task<List<T>> PagedList<T>(IQueryable<T> collection, QueryStringParameters parameters, CancellationToken token = default)
        {
            return await collection
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync(token);
        }

        private void LogRequest(TRequest request, string userId, string userName)
        {
            var requestName = typeof(TRequest).Name;

            Services.Logger.LogInformation("MasterCraft Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, userId, userName, request);
        }

        private void LogPerformance(TRequest request, long elapsedMilliseconds, string userId, string userName)
        {
            if (elapsedMilliseconds > 500)
            {
                var requestName = typeof(TRequest).Name;

                Services.Logger.LogWarning("MasterCraft Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@UserName} {@Request}",
                       requestName, elapsedMilliseconds, userId, userName, request);
            }
        }

        private void LogException(TRequest request, Exception ex)
        {
            var requestName = typeof(TRequest).Name;

            Services.Logger.LogError(ex, "MasterCraft Request: Unhandled Exception for Request {Name} {@Request}", requestName, request);
        }

        private async Task<string> GetUserName(string userId)
        {
            string userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                userName = await Services.IdentityService.GetUserNameAsync(userId);
            }

            return userName;
        }
    }
}
