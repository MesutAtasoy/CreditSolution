using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using Credit.Application.Modules;
using Credit.Contract.IntegrationEvents;
using Credit.Contract.Options;
using Credit.Contract.Producers;
using Credit.Persistence;
using Framework.EntityFrameworkCore;
using Framework.EventBusRabbitMQ;
using Framework.ServiceDiscovery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using RabbitMQ.Client;

namespace Credit.Api.Extensions
{
    /// <summary>
    /// Service Collection Extensions
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        
        /// <summary>
        /// Add Application Settings
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddAppSettings(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var provider = services.BuildServiceProvider())
                configuration = provider.GetService<IConfiguration>();

            services.Configure<CreditApplicationSettings>(configuration.GetSection("app"));
            services.AddTransient(x =>
            {
                var appSettings = new CreditApplicationSettings();
                configuration.GetSection("app").Bind(appSettings);
                return appSettings;
            });

            return services;
        }
        
        /// <summary>
        /// Add api version
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApiVersion(this IServiceCollection services)
            => services.AddApiVersioning(v =>
            {
                v.DefaultApiVersion = new ApiVersion(1, 0);
                v.AssumeDefaultVersionWhenUnspecified = true;
                v.ReportApiVersions = true;
            });

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

            services.AddCustomNpDbContext<CreditApplicationContext>(x =>
            {
                x.EnableMigration = true;
                x.MaxRetryCount = 3;
                x.ConnectionString = configuration["app:ConnectionString"];
            });

            return services;
        }

        /// <summary>
        /// Add Response Compression
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCustomResponseCompression(this IServiceCollection services)
        {
            services.AddResponseCompression();

            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            return services;
        }

        /// <summary>
        /// Add Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                // options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Credit API",
                    Version = "v1",
                    Description = "Credit API",
                    Contact = new OpenApiContact()
                    {
                        Email = "mesutatasoy15@gmail.com",
                        Name = "Mesut Atasoy"
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
                // options.OperationFilter<AuthorizeCheckOperationFilter>();
            });
            return services;
        }

        /// <summary>
        /// Adds Application Modules
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationModules(this IServiceCollection services)
        {
            services.RegisterApplicationModule();
            services.AddMediator();
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


        /// <summary>
        /// Add Producers
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddProducers(this IServiceCollection services)
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            var rabbitMqUrl = configuration["rabbitMQUrl"];
            
            services.AddSingleton<IRabbitMqProducer<CreatedCreditUserRequestEvent>, CreatedCreditUserRequestProducer>()
                .AddSingleton(serviceProvider =>
                {
                    var uri = new Uri(rabbitMqUrl);
                    return new ConnectionFactory
                    {
                        Uri = uri
                    };
                });
            return services;
        }
    }
}