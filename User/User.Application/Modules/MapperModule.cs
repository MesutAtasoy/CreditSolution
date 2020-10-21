using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using User.Application.ViewModelFactories.Base;

namespace User.Application.Modules
{
    public static class MapperModule
    {
        public static IServiceCollection AddMapperModule(this IServiceCollection services)
        {
            IEnumerable<Type> types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(y => typeof(IViewModelFactory).IsAssignableFrom(y) && !y.IsInterface);

            foreach (var type in types)
            {
                services.AddSingleton(type);
            }

            return services;
        }
    }
}