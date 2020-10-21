using Framework.Shared.Models.Base;
using MediatR;

namespace CreditScore.Contract.Base
{
    public interface IBaseRequest : IRequest<BaseResponseModel>
    {
    }
}