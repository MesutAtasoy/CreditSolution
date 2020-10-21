using CreditScore.Domain;
using Framework.EntityFrameworkCore.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CreditScore.Persistence.Configurations
{
    public class UserCreditScoreHistoryConfiguration: BaseEntityModelConfiguration<UserCreditScoreHistory>
    {
        public override void Configure(EntityTypeBuilder<UserCreditScoreHistory> entity)
        {
            entity.Property(e => e.Score)
                .IsRequired()
                .HasMaxLength(250);
            
            entity.HasOne(d => d.UserCreditScore)
                .WithMany(p => p.UserCreditScoreHistories)
                .HasForeignKey(d => d.UserCreditScoreId)
                .HasConstraintName("FK_History_Score");
            
            base.Configure(entity);
        }
    }
}