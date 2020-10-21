using System;
using Consul;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Framework.ServiceDiscovery
{
    public static class ServiceDiscoveryExtensions
    {
        public static void RegisterConsulServices(this IServiceCollection services, Action<ServiceConfigOptions> action)
        {
            var options = new ServiceConfigOptions();
            action.Invoke(options);
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var consulClient = CreateConsulClient(options);

            services.AddSingleton(options);
            services.AddSingleton<IHostedService, ServiceDiscoveryHostedService>();
            services.AddSingleton<IConsulClient, ConsulClient>(p => consulClient);
        }

        private static ConsulClient CreateConsulClient(ServiceConfigOptions serviceConfig)
        {
            return new ConsulClient(config =>
            {
                config.Address = serviceConfig.ServiceDiscoveryAddress;
            });
        }
    }
}