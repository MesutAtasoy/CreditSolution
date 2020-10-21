using System;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using Framework.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using User.Application.Modules;
using User.Persistence;

namespace User.Api.Extensions
{  /// <summary>
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

            var connectionString = configuration["app:ConnectionString"];

            services.AddJokerNpDbContext<UserApplicationContext>(x =>
            {
                x.EnableMigration = true;
                x.MaxRetryCount = 3;
                x.ConnectionString = connectionString;
            });

            return services;
        }

        /// <summary>
        /// Add Response Compression
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddVoyagerResponseCompression(this IServiceCollection services)
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
                IConfiguration configuration;
                using (var serviceProvider = services.BuildServiceProvider())
                {
                    configuration = serviceProvider.GetService<IConfiguration>();
                }

                // options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "User API",
                    Version = "v1",
                    Description = "User API",
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
        /// Adds Voyager Application Modules
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddVoyagerApplicationModules(this IServiceCollection services)
        {
            services.RegisterApplicationModule();
            services.AddMapperModule();
            services.AddMediator();
            return services;
        }
    }
}