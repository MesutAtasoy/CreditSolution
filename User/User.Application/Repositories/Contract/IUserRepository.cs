using System.Threading.Tasks;
using User.Contract.Models.User;

namespace User.Application.Repositories.Contract
{
    public interface IUserRepository
    {
        Task<UserViewModel> GetUserByIdentityNumberAsync(string identityNumber);
    }
}