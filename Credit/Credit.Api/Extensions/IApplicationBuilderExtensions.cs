using Microsoft.AspNetCore.Builder;

namespace Credit.Api.Extensions
{
    /// <summary>
    /// Application Builder Extensions
    /// </summary>
    public static class IApplicationBuilderExtensions
    {
        /// <summary>
        /// Use Swagger and Swagger UI
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Credit API V1");
            });

            return app;
        }
    }
}