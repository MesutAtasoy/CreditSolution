using Microsoft.Extensions.Logging;

namespace User.Persistence.Seed
{
    public class UserApplicationSeedOptions
    {
        public string ContentRootPath { get; set; }
        public UserApplicationContext Context { get; set; }
        public ILogger<UserApplicationContextSeed> Logger { get; set; }
        public int RetryCount { get; set; }
    }
}