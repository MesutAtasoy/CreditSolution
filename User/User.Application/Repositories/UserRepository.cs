using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using User.Application.Repositories.Contract;
using User.Application.ViewModelFactories;
using User.Contract.Models.User;
using User.Persistence;

namespace User.Application.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserApplicationContext _applicationContext;
        private readonly UserViewModelFactory _viewModelFactory;
        
        public UserRepository(UserApplicationContext applicationContext, 
            UserViewModelFactory viewModelFactory)
        {
            _applicationContext = applicationContext;
            _viewModelFactory = viewModelFactory;
        }
        
        public async Task<UserViewModel> GetUserByIdentityNumberAsync(string identityNumber)
        {
            return await _applicationContext.User
                .Where(x => x.IsActive && !x.IsDeleted && x.IdentificationNumber == identityNumber)
                .Select(x => _viewModelFactory.Create(x))
                .FirstOrDefaultAsync();
        }
    }
}