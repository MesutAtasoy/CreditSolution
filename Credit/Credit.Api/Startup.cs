using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Credit.Api.Extensions;
using Framework.Http;
using Framework.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Credit.Api
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// gets configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        
        /// <summary>
        ///This method gets called by the runtime. Use this method to add services to the container. 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServiceDiscovery();
            services.AddAppSettings();
            services.AddContext();
            services.AddHttpClient();
            services.AddBaseHttpClient();
            services.AddApiVersion();
            services.AddCustomResponseCompression();
            services.AddSwagger();
            services.AddApplicationModules();
            services.AddProducers();
            services.AddControllers().AddDefaultJsonOptions();
            services.AddCors(options => options.AddDefaultPolicy(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseCustomSwagger();
            app.UseAllForwardedHeaders();
            app.UseErrorHandler();
            app.UseAuthentication();
            app.UseResponseCompression();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
