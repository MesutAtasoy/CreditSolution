using System;
using System.IO;
using Framework.Logging;
using Framework.WebHost;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using User.Persistence;
using User.Persistence.Seed;

namespace User.Api
{
    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Namespace of application
        /// </summary>
        public static readonly string Namespace = typeof(Program).Namespace;

        /// <summary>
        /// AppName
        /// </summary>
        public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);


        /// <summary>
        /// Main of Program.cs
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static int Main(string[] args)
        {
            var configuration = GetConfiguration();

            Log.Logger = ILoggerBuilderExtensions.CreateLoggerConsole(x =>
            {
                x.AppName = AppName;
                x.Enabled = true;
            });

            try
            {
                Log.Information("Configuring web host ({ApplicationContext})...", AppName);
                var host = BuildWebHost(configuration, args);

                Log.Information("Applying migrations ({ApplicationContext})...", AppName);
                host.MigrateDbContext<UserApplicationContext>((context, services) =>
                {
                    new UserApplicationContextSeed().SeedAsync(options =>
                    {
                        var env = services.GetService<IWebHostEnvironment>();
                        var logger = services.GetService<ILogger<UserApplicationContextSeed>>();

                        options.Context = context;
                        options.ContentRootPath = env.ContentRootPath;
                        options.Logger = logger;
                        options.RetryCount = 5;
                    }, services).Wait();
                });
                host.Run();
                return 0;
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Program terminated unexpectedly ({ApplicationContext})!", AppName);
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();

            }
        }

        private static IWebHost BuildWebHost(IConfiguration configuration, string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .CaptureStartupErrors(false)
                .UseStartup<Startup>()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(configuration)
                .UseSerilog()
                .Build();

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}