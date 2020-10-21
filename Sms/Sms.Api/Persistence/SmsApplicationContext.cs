using Microsoft.EntityFrameworkCore;
using Sms.Api.Persistence.Models;

namespace Sms.Api
{
    public class SmsApplicationContext : DbContext
    {
        public SmsApplicationContext()
        {
        }

        public SmsApplicationContext(DbContextOptions<SmsApplicationContext> options)
            : base(options)
        {
        }
        
        public DbSet<SmsLog> SmsLogs { get; set; }
    }
}