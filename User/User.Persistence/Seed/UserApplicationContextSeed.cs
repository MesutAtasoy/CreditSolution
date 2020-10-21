using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using User.Persistence.Seed.Seeders.Base;

namespace User.Persistence.Seed
{
  public class UserApplicationContextSeed
    {
        public async Task SeedAsync(Action<UserApplicationSeedOptions> action, IServiceProvider serviceProvider)
        {
            var options = new UserApplicationSeedOptions();
            action.Invoke(options);
            CheckNull(options);

            try
            {
                var context = options.Context;
                var seeders = GetClassByType<ISeeder>();
                var tasks = seeders.OrderBy(o => o.Order).Where(x=>x.Order.HasValue).ToList();
                foreach (var seeder in tasks)
                {
                    await seeder.SeedAsync(context, options.ContentRootPath, options.Logger, serviceProvider);
                }
                
                await context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                options.Logger.LogError(ex, "EXCEPTION ERROR while migrating {DbContextName}", nameof(UserApplicationSeedOptions));
                await SeedAsync(action, serviceProvider);
            }
        }

        #region Utilies Functions

        private void CheckNull(UserApplicationSeedOptions options)
        {
            if (string.IsNullOrEmpty(options.ContentRootPath))
                throw new ArgumentNullException(options.ContentRootPath);
            if (options.Context == null)
                throw new ArgumentNullException(nameof(options.Context));
            if (options.RetryCount == default)
                options.RetryCount = 5;
        }

        private IList<T> GetClassByType<T>()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(T).IsAssignableFrom(p) && !p.IsAbstract && !p.IsInterface)
                .Select(c => (T) Activator.CreateInstance(c))
                .ToList();
        }

        #endregion
    }
}