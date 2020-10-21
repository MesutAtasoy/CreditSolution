using CreditScore.Application.Decarators;
using CreditScore.Application.Repositories;
using CreditScore.Application.Repositories.Contract;
using Framework.DispatchProxy;
using Microsoft.Extensions.DependencyInjection;

namespace CreditScore.Application.Modules
{
    public static class ApplicationModule
    {
        public static IServiceCollection RegisterApplicationModule(this IServiceCollection services)
        {
            services.AddTransient<IUserCreditScoreRepository, UserCreditScoreRepository>();
            return services;
        }
        

        public static IServiceCollection RegisterDispatchProxies(this IServiceCollection services)
        {
            services.DecorateWithDispatchProxy<IUserCreditScoreRepository, CustomDistributedCacheDecorator<IUserCreditScoreRepository>>();
            return services;
        }
    }
}