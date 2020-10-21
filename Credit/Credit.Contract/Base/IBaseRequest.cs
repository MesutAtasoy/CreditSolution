using Framework.Shared.Models.Base;
using MediatR;

namespace Credit.Contract.Base
{
    public interface IBaseRequest : IRequest<BaseResponseModel>
    {
    }
}