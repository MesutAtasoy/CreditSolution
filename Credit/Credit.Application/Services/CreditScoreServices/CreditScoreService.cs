using System.Threading.Tasks;
using Credit.Application.Services.CreditScoreServices.Models;
using Credit.Contract.Options;
using Framework.Http;
using Framework.Shared.Models.Base;
using Microsoft.Extensions.Options;

namespace Credit.Application.Services.CreditScoreServices
{
    public class CreditScoreService : ICreditScoreService
    {
        private readonly IBaseHttpClientWrapper _clientWrapper;
        private readonly IOptions<CreditApplicationSettings> _options;
        private readonly CreditApplicationSettings _settings;
        
        public CreditScoreService(IBaseHttpClientWrapper clientWrapper, 
            CreditApplicationSettings settings, IOptions<CreditApplicationSettings> options)
        {
            _clientWrapper = clientWrapper;
            _options = options;
            _settings = options.Value;
        }
        
        public async Task<BaseResponseModel<UserCreditScoreViewModel>> GetScoreByIdentityNumberAsync(string identityNumber)
        {
            var response = await _clientWrapper.GetAsync<UserCreditScoreViewModel>($"{_settings.CreditScoreUrl}/api/v1/UserCredit/ByIdentityNumber?IdentityNumber={identityNumber}" );
            return response;
        }
    }
}