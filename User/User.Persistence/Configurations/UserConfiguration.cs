using Framework.EntityFrameworkCore.Configurations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace User.Persistence.Configurations
{
    public class UserConfiguration: BaseEntityModelConfiguration<Domain.User>
    {
        public override void Configure(EntityTypeBuilder<Domain.User> entity)
        {
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(250);
            
            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(100);
            
            entity.Property(e => e.IdentificationNumber)
                .IsRequired()
                .HasMaxLength(50);
            
            base.Configure(entity);
        }
    }
}