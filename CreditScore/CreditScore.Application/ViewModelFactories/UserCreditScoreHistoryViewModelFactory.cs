using CreditScore.Application.ViewModelFactories.Base;
using CreditScore.Contract.Models.UserCreditScore;
using CreditScore.Domain;

namespace CreditScore.Application.ViewModelFactories
{
    public class UserCreditScoreHistoryViewModelFactory : IViewModelFactory
    {
        public UserCreditScoreHistoryViewModel Create(UserCreditScoreHistory score)
        {
            return new UserCreditScoreHistoryViewModel
            {
                Id = score.Id,
                CreatedBy = score.CreatedBy,
                CreatedOnUtc = score.CreatedOnUtc,
                UpdatedOnUtc = score.UpdatedOnUtc,
                UpdatedBy = score.UpdatedBy,
                IsActive = score.IsActive,
                IsDeleted = score.IsDeleted,
                Score = score.Score
            };
        }
    }
}