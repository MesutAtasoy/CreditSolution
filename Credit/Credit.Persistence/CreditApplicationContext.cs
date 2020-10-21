using Credit.Domain;
using Microsoft.EntityFrameworkCore;

namespace Credit.Persistence
{
    public class CreditApplicationContext : DbContext
    {
        public CreditApplicationContext()
        {
        }

        public CreditApplicationContext(DbContextOptions<CreditApplicationContext> options)
            : base(options)
        {
        }
        
        public DbSet<UserCreditRequest> UserCreditRequests { get; set; }
    }
}