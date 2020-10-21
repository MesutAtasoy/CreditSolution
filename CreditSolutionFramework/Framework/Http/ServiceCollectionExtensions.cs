using Microsoft.Extensions.DependencyInjection;

namespace Framework.Http
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddBaseHttpClient(this IServiceCollection services)
        {
            services.AddHttpClient<IBaseHttpClientWrapper, BaseHttpClientWrapper>();
            return services;
        }
    }
}