using System.Threading;
using System.Threading.Tasks;
using Framework.Shared.Models.Base;
using MediatR;
using User.Application.Repositories.Contract;
using User.Contract.Queries.Users.GetUserByIdentityNumber;

namespace User.Application.Queries.Users.GetUserByIdentityNumber
{
    public class GetUserByIdentityNumberQueryHandler: IRequestHandler<GetUserByIdentityNumberQuery, BaseResponseModel>, IQuery
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdentityNumberQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponseModel> Handle(GetUserByIdentityNumberQuery request, CancellationToken cancellationToken)
        {
            var response = new BaseResponseModel
            {
                Payload = await _userRepository.GetUserByIdentityNumberAsync(request.IdentityNumber)
            };
            return response;
        }
    }
}