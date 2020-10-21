using Framework.Shared.Models.Base;
using MediatR;

namespace User.Contract.Base
{
    public interface IBaseRequest : IRequest<BaseResponseModel>
    {
    }
}