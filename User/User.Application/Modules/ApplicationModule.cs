using Microsoft.Extensions.DependencyInjection;
using User.Application.Repositories;
using User.Application.Repositories.Contract;

namespace User.Application.Modules
{
    public static class ApplicationModule
    {
        public static IServiceCollection RegisterApplicationModule(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }
    }
}