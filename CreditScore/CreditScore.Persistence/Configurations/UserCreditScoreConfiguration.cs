using CreditScore.Domain;
using Framework.EntityFrameworkCore.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditScore.Persistence.Configurations
{
    public class UserCreditScoreConfiguration: BaseEntityModelConfiguration<UserCreditScore>
    {
        public override void Configure(EntityTypeBuilder<UserCreditScore> entity)
        {
            entity.Property(e => e.Score)
                .IsRequired()
                .HasMaxLength(250);
            
            entity.Property(e => e.IdentityNumber)
                .HasMaxLength(250)
                .IsRequired();
            
            base.Configure(entity);
        }
    }
}