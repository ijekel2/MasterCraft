using MasterCraft.Domain.Common.RequestHandling;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace MasterCraft.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddRequestHandlers();
			services.AddTransient<RequestHandlerService>();

            return services;
        }

		private static void AddRequestHandlers(this IServiceCollection pServices)
		{
			//-- Register all request handlers
			Type baseType = typeof(RequestHandler<,>);

			Assembly assembly = baseType.Assembly;

			foreach (TypeInfo lTypeInfo in assembly.DefinedTypes)
			{
				Type handlerType = lTypeInfo.AsType();
				if (IsRequestHandlerType(handlerType, baseType))
				{
					var type = lTypeInfo.AsType();
					pServices.AddTransient(type);
				}
			}
		}

		private static bool IsRequestHandlerType(Type handlerType, Type baseType)
        {
			return handlerType.BaseType is not null && handlerType.BaseType.IsGenericType && handlerType.BaseType.GetGenericTypeDefinition().Equals(baseType);

		}
	}
}
