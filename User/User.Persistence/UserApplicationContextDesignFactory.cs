using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace User.Persistence
{
    public class UserApplicationContextDesignFactory : IDesignTimeDbContextFactory<UserApplicationContext>
    {
        public UserApplicationContextDesignFactory()
        {
        }
        public UserApplicationContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            var configuration = builder.Build();
            var optionsBuilder = new DbContextOptionsBuilder<UserApplicationContext>();
            var connectionString = configuration["app:ConnectionString"];
            optionsBuilder.UseNpgsql(connectionString);
            return new UserApplicationContext(optionsBuilder.Options);
        }
    }
}