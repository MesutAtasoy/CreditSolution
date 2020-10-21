using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Sms.Api.Persistence;

namespace Sms.Api
{
    public class SmsApplicationContextDesignFactory : IDesignTimeDbContextFactory<SmsApplicationContext>
    {
        public SmsApplicationContextDesignFactory()
        {
        }
        public SmsApplicationContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<SmsApplicationContext>();
            var connectionString = configuration["app:ConnectionString"];
            optionsBuilder.UseNpgsql(connectionString);
            return new SmsApplicationContext(optionsBuilder.Options);
        }
    }
}