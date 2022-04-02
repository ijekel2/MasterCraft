using MasterCraft.Domain.Services;
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
			services.AddTransient<ServiceDependencies>();

            return services;
        }

		private static void AddRequestHandlers(this IServiceCollection pServices)
		{
			//-- Register all request services
			Type baseType = typeof(DomainService<,>);

			Assembly assembly = baseType.Assembly;

			foreach (TypeInfo lTypeInfo in assembly.DefinedTypes)
			{
				Type serviceType = lTypeInfo.AsType();
				if (IsRequestHandlerType(serviceType, baseType))
				{
					var type = lTypeInfo.AsType();
					pServices.AddTransient(type);
				}
			}
		}

		private static bool IsRequestHandlerType(Type serviceType, Type baseType)
        {
			return serviceType.BaseType is not null && serviceType.BaseType.IsGenericType && serviceType.BaseType.GetGenericTypeDefinition().Equals(baseType);

		}
	}
}
