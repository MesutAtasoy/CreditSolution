using System;
using System.Threading.Tasks;
using CreditScore.Contract.Constants;
using CreditScore.Contract.Models.UserCreditScore;
using CreditScore.Domain;
using Framework.Cache.Attributes;

namespace CreditScore.Application.Repositories.Contract
{
    public interface IUserCreditScoreRepository
    {
        [Cache(Key = CreditScoreByIdentityNumberCacheKeys.ByIdentityNumber.Key, TTL = CreditScoreByIdentityNumberCacheKeys.ByIdentityNumber.TTL )]
        Task<UserCreditScore> GetUserCreditScoreByIdentityNumberAsync(string identityNumber);
    }
}