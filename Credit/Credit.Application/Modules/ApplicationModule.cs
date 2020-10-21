using Credit.Application.Repositories;
using Credit.Application.Repositories.Contract;
using Credit.Application.Services.CreditScoreServices;
using Microsoft.Extensions.DependencyInjection;

namespace Credit.Application.Modules
{
    public static class ApplicationModule
    {
        public static IServiceCollection RegisterApplicationModule(this IServiceCollection services)
        {
            services.AddTransient<ICreditScoreService, CreditScoreService>();
            services.AddTransient<IUserCreditRequestRepository, UserCreditRequestRepository>();
            return services;
        }
    }
}