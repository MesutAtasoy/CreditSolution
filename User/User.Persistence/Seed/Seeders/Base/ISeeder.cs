using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace User.Persistence.Seed.Seeders.Base
{
    public interface ISeeder
    {
        public int? Order { get; }
        Task SeedAsync(UserApplicationContext context, string contentRootPath, ILogger<UserApplicationContextSeed> logger, IServiceProvider serviceProvider);
    }
}