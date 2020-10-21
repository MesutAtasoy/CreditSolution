using System;
using System.Threading.Tasks;
using CreditScore.Persistence.Seed;
using Microsoft.Extensions.Logging;

namespace CreditScore.Persistence.Seed.Seeders.Base
{
    public interface ISeeder
    {
        public int? Order { get; }
        Task SeedAsync(CreditScoreApplicationContext context, string contentRootPath, ILogger<CreditScoreApplicationContextSeed> logger, IServiceProvider serviceProvider);
    }
}