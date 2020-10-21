using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CreditScore.Domain;
using CreditScore.Persistence.Seed.Seeders.Base;

using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CreditScore.Persistence.Seed.Seeders
{
    public class UserCreditScoreSeeder : ISeeder
    {
        public int? Order => 1;
        public Task SeedAsync(CreditScoreApplicationContext context, string contentRootPath, ILogger<CreditScoreApplicationContextSeed> logger, IServiceProvider serviceProvider)
        {
            if (context.UserCreditScores.Any())
                return Task.FromResult(0);

            logger.LogInformation("User Credit Score Seeder is working");

            return Task.FromResult(context.UserCreditScores.AddRangeAsync((LoadUserCreditScores(contentRootPath))));
        }
        
        private List<UserCreditScore> LoadUserCreditScores(string contentRootPath)
            => Load<List<UserCreditScore>>(contentRootPath, "Data/userCreditScore.json");

        private T Load<T>(string contentRootPath, string fileLocation)
        {
            var currencyJson = File.ReadAllText(Path.Combine(contentRootPath, fileLocation));
            return JsonConvert.DeserializeObject<T>(currencyJson);
        }
    }
}