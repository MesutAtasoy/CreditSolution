using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Credit.Persistence
{
    public class CreditApplicationContextDesignFactory : IDesignTimeDbContextFactory<CreditApplicationContext>
    {
        public CreditApplicationContextDesignFactory()
        {
        }
        
        public CreditApplicationContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<CreditApplicationContext>();
            var connectionString = configuration["app:ConnectionString"];
            optionsBuilder.UseNpgsql(connectionString);
            return new CreditApplicationContext(optionsBuilder.Options);
        }
    }
}