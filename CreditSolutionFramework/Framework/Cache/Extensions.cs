﻿using System;
using Microsoft.Extensions.DependencyInjection;

namespace Framework.Cache
{
    public static class Extensions
    {
        public static IServiceCollection AddCustomCache(this IServiceCollection services, Action<CustomCacheOptions> options)
        {
            var cacheOptions = new CustomCacheOptions(); 
            
            options.Invoke(cacheOptions);
            
            if(string.IsNullOrEmpty(cacheOptions.Instance))
                throw new ArgumentNullException(nameof(cacheOptions.Instance));
            
            if(string.IsNullOrEmpty(cacheOptions.ConnectionString))
                throw new ArgumentNullException(nameof(cacheOptions.ConnectionString));
                
            services.AddDistributedRedisCache(o =>
            {
                o.Configuration = cacheOptions.ConnectionString;
                o.InstanceName = cacheOptions.Instance;
            });

            services.AddTransient<ICustomDistributedCache, CustomDistributedCache>();
            return services;
        }
    }
}
