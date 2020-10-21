using System;
using System.Collections.Generic;
using System.Drawing;
using Framework.EntityFrameworkCore.Models;

namespace CreditScore.Domain
{
    public class UserCreditScore : BaseEntityModel
    {
        public UserCreditScore()
        {
            UserCreditScoreHistories = new HashSet<UserCreditScoreHistory>();
        }
        
        public string IdentityNumber { get; set; }
        public int Score { get; set; }
        public virtual ICollection<UserCreditScoreHistory> UserCreditScoreHistories { get; set; }
    }
}