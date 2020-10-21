using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using User.Application.Queries;

namespace User.Application.Modules
{
    public static class MediatorModule
    {
        public static IServiceCollection AddMediator(this IServiceCollection services)
        {
            services.AddMediatR(typeof(IQuery).GetTypeInfo().Assembly);
            return services;
        }
    }
}