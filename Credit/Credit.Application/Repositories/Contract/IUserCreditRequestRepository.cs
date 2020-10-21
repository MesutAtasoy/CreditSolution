using System.Threading.Tasks;
using Credit.Domain;

namespace Credit.Application.Repositories.Contract
{
    public interface IUserCreditRequestRepository
    {
        Task AddRequestAsync(UserCreditRequest request);
    }
}