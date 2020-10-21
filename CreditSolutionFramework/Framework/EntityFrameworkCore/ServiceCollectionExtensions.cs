using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using System;
using Framework.EntityFrameworkCore.OptionsBuilders;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.EntityFrameworkCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCustomDbContext<TContext>(this IServiceCollection services, Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction)
            where TContext : DbContext
        {
            IConfiguration configuration;
            using (var serviceProvider = services.BuildServiceProvider())
            {
                configuration = serviceProvider.GetService<IConfiguration>();
            }

            services.AddDbContext<TContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionString"], sqlServerOptionsAction);
            });

            return services;
        }

        public static IServiceCollection AddCustomDbContext<TContext>(this IServiceCollection services, Action<CustomDbContextOptionBuilder> optionBuilder)
           where TContext : DbContext
        {
            CustomDbContextOptionBuilder contextOptionBuilder = new CustomDbContextOptionBuilder();
            optionBuilder.Invoke(contextOptionBuilder);

            if (string.IsNullOrEmpty(contextOptionBuilder.ConnectionString))
                throw new ArgumentNullException("Connectionstring can not be null", nameof(contextOptionBuilder.ConnectionString));

            string assemblyName = typeof(TContext).Namespace;

            services.AddDbContext<TContext>(options =>
            {
                options.UseSqlServer(contextOptionBuilder.ConnectionString, sqlOptions =>
                {
                    if (contextOptionBuilder.EnableMigration)
                    {
                        sqlOptions.MigrationsAssembly(assemblyName);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: contextOptionBuilder.MaxRetryCount,
                            maxRetryDelay: contextOptionBuilder.MaxRetryDelay ?? TimeSpan.FromSeconds(30),
                            errorNumbersToAdd: null);
                    }
                });
            });
            return services;
        }

        public static IServiceCollection AddCustomNpDbContext<TContext>(this IServiceCollection services, Action<CustomNpDbContextOptionBuilder> optionBuilder)
         where TContext : DbContext
        {
            var contextOptionBuilder = new CustomNpDbContextOptionBuilder();
            optionBuilder.Invoke(contextOptionBuilder);

            if (string.IsNullOrEmpty(contextOptionBuilder.ConnectionString))
                throw new ArgumentNullException(nameof(ServiceCollectionExtensions), nameof(contextOptionBuilder.ConnectionString));

            var assemblyName = typeof(TContext).Namespace;

            services.AddDbContext<TContext>(options =>
            {
                options.UseNpgsql(contextOptionBuilder.ConnectionString, sqlOptions =>
                {
                    if (contextOptionBuilder.EnableMigration)
                        sqlOptions.MigrationsAssembly(assemblyName);
                });
            });

            return services;
        }
    }
}

