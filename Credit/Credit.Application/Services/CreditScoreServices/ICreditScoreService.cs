using System.Threading.Tasks;
using Credit.Application.Services.CreditScoreServices.Models;
using Framework.Shared.Models.Base;

namespace Credit.Application.Services.CreditScoreServices
{
    public interface ICreditScoreService
    {
        Task<BaseResponseModel<UserCreditScoreViewModel>> GetScoreByIdentityNumberAsync(string identityNumber);
    }
}