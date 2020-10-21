using System.Reflection;
using CreditScore.Application.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CreditScore.Application.Modules
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