using System.Collections.Generic;
using System.Linq;
using CreditScore.Application.ViewModelFactories.Base;
using CreditScore.Contract.Models.UserCreditScore;
using CreditScore.Domain;

namespace CreditScore.Application.ViewModelFactories
{
    public class UserCreditScoreViewModelFactory : IViewModelFactory
    {
        private readonly UserCreditScoreHistoryViewModelFactory _factory;
        
        public UserCreditScoreViewModelFactory(UserCreditScoreHistoryViewModelFactory factory)
        {
            _factory = factory;
        }
        public UserCreditScoreViewModel Create(UserCreditScore score)
        {
            return new UserCreditScoreViewModel
            {
                Id = score.Id,
                CreatedBy = score.CreatedBy,
                CreatedOnUtc = score.CreatedOnUtc,
                UpdatedOnUtc = score.UpdatedOnUtc,
                UpdatedBy = score.UpdatedBy,
                IsActive = score.IsActive,
                IsDeleted = score.IsDeleted,
                Score = score.Score,
                IdentityNumber = score.IdentityNumber,
                UserCreditScoreHistories =  new List<UserCreditScoreHistoryViewModel>() 
            };
        }
    }
}