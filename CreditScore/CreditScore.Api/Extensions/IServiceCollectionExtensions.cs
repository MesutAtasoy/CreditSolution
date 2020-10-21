using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using CreditScore.Application.Modules;
using CreditScore.Persistence;
using Framework.EntityFrameworkCore;
using Framework.ServiceDiscovery;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CreditScore.Api.Extensions
{
    /// <summary>
    /// Service Collection Extensions
    /// </summary>
    public static class ServiceCollectionExtensions
    {
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

            services.AddCustomNpDbContext<CreditScoreApplicationContext>(x =>
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
        /// Add Swagger
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                IConfiguration configuration;
                using (var serviceProvider = services.BuildServiceProvider())
                {
                    configuration = serviceProvider.GetService<IConfiguration>();
                }

                // options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Credit Score API",
                    Version = "v1",
                    Description = "Credit Score API",
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
        public static IServiceCollection AddCustomApplicationModules(this IServiceCollection services)
        {
            services.RegisterApplicationModule();
            services.AddMapperModule();
            services.AddMediator();
            services.RegisterDispatchProxies();
            return services;
        }
    }
}