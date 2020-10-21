using Microsoft.Extensions.Logging;

namespace CreditScore.Persistence.Seed
{
    public class CreditScoreApplicationSeedOptions
    {
        public string ContentRootPath { get; set; }
        public CreditScoreApplicationContext Context { get; set; }
        public ILogger<CreditScoreApplicationContextSeed> Logger { get; set; }
        public int RetryCount { get; set; }
    }
}