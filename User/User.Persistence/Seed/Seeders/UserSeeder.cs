using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using User.Persistence.Seed.Seeders.Base;
using System.Linq;

namespace User.Persistence.Seed.Seeders
{
    public class UserSeeder : ISeeder
    {
        public int? Order => 1;

        public Task SeedAsync(UserApplicationContext context, string contentRootPath, ILogger<UserApplicationContextSeed> logger,
            IServiceProvider serviceProvider)
        {
            if (context.User.Any())
                return Task.FromResult(0);

            logger.LogInformation("User Seeder is working");

            return Task.FromResult(context.User.AddRangeAsync(LoadUsers(contentRootPath)));
        }
        
        private List<Domain.User> LoadUsers(string contentRootPath)
            => Load<List<Domain.User>>(contentRootPath, "Data/users.json");

        private T Load<T>(string contentRootPath, string fileLocation)
        {
            var currencyJson = File.ReadAllText(Path.Combine(contentRootPath, fileLocation));
            return JsonConvert.DeserializeObject<T>(currencyJson);
        }
    }
}