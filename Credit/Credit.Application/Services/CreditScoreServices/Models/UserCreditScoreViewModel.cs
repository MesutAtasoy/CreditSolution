using System.Collections.Generic;
using Credit.Contract.Models.Base;

namespace Credit.Application.Services.CreditScoreServices.Models
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