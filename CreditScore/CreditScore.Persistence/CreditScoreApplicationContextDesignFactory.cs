using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CreditScore.Persistence
{
    public class CreditScoreApplicationContextDesignFactory : IDesignTimeDbContextFactory<CreditScoreApplicationContext>
    {
        public CreditScoreApplicationContextDesignFactory()
        {
        }
        public CreditScoreApplicationContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<CreditScoreApplicationContext>();
            var connectionString = configuration["app:ConnectionString"];
            optionsBuilder.UseNpgsql(connectionString);
            return new CreditScoreApplicationContext(optionsBuilder.Options);
        }
    }
}