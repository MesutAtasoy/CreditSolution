using System.Threading.Tasks;
using Credit.Application.Repositories.Contract;
using Credit.Domain;
using Credit.Persistence;

namespace Credit.Application.Repositories
{
    public class UserCreditRequestRepository : IUserCreditRequestRepository
    {
        private readonly CreditApplicationContext _context;
        
        public UserCreditRequestRepository(CreditApplicationContext context)
        {
            _context = context;
        }
        
        public async Task AddRequestAsync(UserCreditRequest request)
        {
            await _context.UserCreditRequests.AddAsync(request);
            await _context.SaveChangesAsync();
        }
    }
}