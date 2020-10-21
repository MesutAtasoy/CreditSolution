using System;
using System.IO;
using System.Reflection;
using Framework.EntityFrameworkCore;
using Framework.EventBusRabbitMQ;
using Framework.ServiceDiscovery;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;
using Sms.Api.Consumers;
using Sms.Api.EventHandlers;
using Sms.Api.Events;
using Sms.Api.Persistence;

namespace Sms.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
   
        /// <summary>
        /// Add Db Context
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddContext(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            services.AddCustomNpDbContext<SmsApplicationContext>(x =>
            {
                x.EnableMigration = true;
                x.MaxRetryCount = 3;
                x.ConnectionString = configuration["app:ConnectionString"];
            });

            return services;
        }
       
        /// <summary>
        /// Add Producers
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddConsumers(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient<IRequestHandler<CreatedCreditUserRequestEvent, Unit>, CreatedCreditUserRequestEventHandler>();
            services.AddHostedService<CreatedCreditUserRequestConsumer>();
            
            var rabbitMqUrl = configuration["rabbitMQUrl"];

            services.AddSingleton(serviceProvider =>
            {
                var uri = new Uri(rabbitMqUrl);
                return new ConnectionFactory
                {
                    Uri = uri,
                    DispatchConsumersAsync = true
                };
            });
            return services;
        }
        
        /// <summary>
        /// Adds Service Discovery
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddServiceDiscovery(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }
            
            services.RegisterConsulServices(x =>
            {
                x.ServiceAddress = new Uri(configuration["app:serviceConfig:serviceAddress"]);
                x.ServiceDiscoveryAddress = new Uri(configuration["app:serviceConfig:serviceDiscoveryAddress"]);
                x.ServiceId = configuration["app:serviceConfig:serviceId"];
                x.ServiceName = configuration["app:serviceConfig:serviceName"];
            });
            return services;
        }

    }
}