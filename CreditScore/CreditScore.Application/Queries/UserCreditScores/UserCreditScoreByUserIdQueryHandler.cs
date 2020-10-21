using System.Threading;
using System.Threading.Tasks;
using CreditScore.Application.Repositories.Contract;
using CreditScore.Application.ViewModelFactories;
using CreditScore.Contract.Queries.UserCreditScore.UserCreditScoreByUserId;
using Framework.Exceptions;
using Framework.Shared.Models.Base;
using Framework.Shared.Models.Enums;
using MediatR;

namespace CreditScore.Application.Queries.UserCreditScores
{
    public class UserCreditScoreByUserIdQueryHandler: IRequestHandler<UserCreditScoreByUserIdQuery, BaseResponseModel>, IQuery
    {
        private readonly IUserCreditScoreRepository _creditScoreRepository;
        private readonly UserCreditScoreViewModelFactory _modelFactory;

        public UserCreditScoreByUserIdQueryHandler(IUserCreditScoreRepository creditScoreRepository, 
            UserCreditScoreViewModelFactory modelFactory)
        {
            _creditScoreRepository = creditScoreRepository;
            _modelFactory = modelFactory;
        }

        public async Task<BaseResponseModel> Handle(UserCreditScoreByUserIdQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseModel();

            var score = await _creditScoreRepository.GetUserCreditScoreByIdentityNumberAsync(request.IdentityNumber);
            if (score == null)
            {
                throw new ApiException("User Score is not found", ResponseMessageType.NotFound, null, 404);
            }

            response.Payload = _modelFactory.Create(score);            
            return response;
        }
    }
}