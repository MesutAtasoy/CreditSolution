using System.Threading.Tasks;
using Credit.Api.Controllers.Base;
using Credit.Contract.Commands.UserCreditRequest.CreateUserCreditRequest;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Credit.Api.Controllers
{
    /// <summary>
    /// Controller
    /// </summary>
    public class CreditController : BaseController
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediator"></param>
        public CreditController(IMediator mediator)
            : base(mediator)
        {
        }
        
        /// <summary>
        /// Creates user credit request
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("CreateRequest")]
        [ApiVersion("1")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateRequest([FromBody] CreateUserCreditRequestCommand command)
            => Ok(await Mediator.Send(command));
    }
}