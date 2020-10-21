using System;
using System.Collections.Generic;
using System.Linq;
using CreditScore.Application.ViewModelFactories.Base;
using Microsoft.Extensions.DependencyInjection;

namespace CreditScore.Application.Modules
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