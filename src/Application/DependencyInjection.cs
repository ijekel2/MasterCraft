using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace MasterCraft.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddRequestHandlers();
			services.AddTransient<Common.Utilities.Mediator>();

            return services;
        }

		private static void AddRequestHandlers(this IServiceCollection pServices)
		{
			//-- Register all IPopCall implementations
			Type interfaceType = typeof(Common.Interfaces.IRequestHandler<,>);

			Assembly assembly = interfaceType.Assembly;

			foreach (TypeInfo lTypeInfo in assembly.DefinedTypes)
			{
				foreach (Type implementedInterface in lTypeInfo.ImplementedInterfaces)
                {
					if (implementedInterface.IsGenericType && implementedInterface.GetGenericTypeDefinition() == interfaceType)
					{
						var type = lTypeInfo.AsType();
						pServices.AddTransient(type);
					}
				}
			}
		}
	}
}
