using System;
using System.Collections.Generic;
using CreditScore.Contract.Models.Base;

namespace CreditScore.Contract.Models.UserCreditScore
{
    public class UserCreditScoreViewModel : BaseViewModel
    {
        public string IdentityNumber { get; set; } 
        public int Score { get; set; }
        public List<UserCreditScoreHistoryViewModel> UserCreditScoreHistories { get; set; }
    }

    public class UserCreditScoreHistoryViewModel : BaseViewModel
    {
        public int Score { get; set; }
    }
}