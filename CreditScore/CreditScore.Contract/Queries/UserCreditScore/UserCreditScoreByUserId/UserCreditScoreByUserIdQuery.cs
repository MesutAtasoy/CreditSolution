using System;
using CreditScore.Contract.Base;

namespace CreditScore.Contract.Queries.UserCreditScore.UserCreditScoreByUserId
{
    public class UserCreditScoreByUserIdQuery: IBaseRequest
    {
        public string IdentityNumber { get; set; }
    }
}