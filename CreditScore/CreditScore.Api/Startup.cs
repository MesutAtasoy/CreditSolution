using CreditScore.Api.Extensions;
using Framework.Cache;
using Framework.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CreditScore.Api
{
    /// <summary>
    /// Startup Class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///This method gets called by the runtime. Use this method to add services to the container. 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServiceDiscovery();
            services.AddContext();
            services.AddHttpClient();
            services.AddApiVersion();
            services.AddCustomResponseCompression();
            services.AddCustomCache(options => Configuration.GetSection("app:cache").Bind(options));
            services.AddSwagger();
            services.AddCustomApplicationModules();
            services.AddControllers().AddDefaultJsonOptions();
            
        }

        /// <summary>
        ///This method gets called by the runtime. Use this method to configure the HTTP request pipeline. 
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
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
