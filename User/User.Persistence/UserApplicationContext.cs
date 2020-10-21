using Microsoft.EntityFrameworkCore;

namespace User.Persistence
{
    public class UserApplicationContext : DbContext
    {
        
        public UserApplicationContext()
        {
        }

        public UserApplicationContext(DbContextOptions<UserApplicationContext> options)
            : base(options)
        {
        }
        
        public virtual DbSet<Domain.User> User { get; set; }
    }
}