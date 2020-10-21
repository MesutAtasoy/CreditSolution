using CreditScore.Domain;
using Microsoft.EntityFrameworkCore;

namespace CreditScore.Persistence
{
    public class CreditScoreApplicationContext : DbContext
    {
        public CreditScoreApplicationContext()
        {
        }

        public CreditScoreApplicationContext(DbContextOptions<CreditScoreApplicationContext> options)
            : base(options)
        {
        }
        
        public DbSet<UserCreditScore> UserCreditScores { get; set; }
        public DbSet<UserCreditScoreHistory> UserCreditScoreHistories { get; set; }
    }
}