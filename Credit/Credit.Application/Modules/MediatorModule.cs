using System.Reflection;
using Credit.Application.Commands;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Credit.Application.Modules
{
    public static class MediatorModule
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(typeof(ICommand).GetTypeInfo().Assembly);
            return services;
        }
    }
}