using System;
using Framework.EntityFrameworkCore.Models;

namespace CreditScore.Domain
{
    public class UserCreditScoreHistory : BaseEntityModel
    {
        public int Score { get; set; }
        public Guid UserCreditScoreId { get; set; }
        public virtual UserCreditScore  UserCreditScore { get; set; }
    }
}