using Credit.Domain;
using Framework.EntityFrameworkCore.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Credit.Persistence.Configurations
{
    public class UserCreditRequestConfiguration: BaseEntityModelConfiguration<UserCreditRequest>
    {
        public override void Configure(EntityTypeBuilder<UserCreditRequest> entity)
        {
            entity.Property(e => e.IdentityNumber)
                .HasMaxLength(250)
                .IsRequired();
            
            entity.Property(e => e.Name)
                .HasMaxLength(250)
                .IsRequired();
            
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(250)
                .IsRequired();
            
            entity.Property(e => e.MonthlyIncome)
                .IsRequired();

            entity.Property(e => e.CreditLimit);
            
            entity.Property(e => e.CreditLimitMultiplier);
            
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsRequired();
            
            base.Configure(entity);
        }
    }
}