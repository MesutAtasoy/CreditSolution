using System;
using System.Linq;
using System.Threading.Tasks;
using CreditScore.Application.Repositories.Contract;
using CreditScore.Application.ViewModelFactories;
using CreditScore.Contract.Models.UserCreditScore;
using CreditScore.Domain;
using CreditScore.Persistence;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace CreditScore.Application.Repositories
{
    public class UserCreditScoreRepository : IUserCreditScoreRepository
    {
        private readonly CreditScoreApplicationContext _context;
        private readonly UserCreditScoreViewModelFactory _factory;

        public UserCreditScoreRepository(CreditScoreApplicationContext context,
            UserCreditScoreViewModelFactory factory)
        {
            _context = context;
            _factory = factory;
        }

        public async Task<UserCreditScore> GetUserCreditScoreByIdentityNumberAsync(string identityNumber)
        {
            var score = await _context.UserCreditScores
                .FirstOrDefaultAsync(x => x.IsActive && !x.IsDeleted && x.IdentityNumber == identityNumber);
            
            return score;
        }
    }
}