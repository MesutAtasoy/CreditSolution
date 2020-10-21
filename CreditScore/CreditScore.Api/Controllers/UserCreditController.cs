using System.Threading.Tasks;
using CreditScore.Api.Controllers.Base;
using CreditScore.Contract.Queries.UserCreditScore.UserCreditScoreByUserId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CreditScore.Api.Controllers
{
    /// <summary>
    /// Controller
    /// </summary>
    public class UserCreditController : BaseController
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediator"></param>
        public UserCreditController(IMediator mediator)
            : base(mediator)
        {
        }
        
        /// <summary>
        /// Returns credit score by identity number
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("ByIdentityNumber")]
        [ApiVersion("1")]
        [AllowAnonymous]
        public async Task<IActionResult> ByUserId([FromQuery] UserCreditScoreByUserIdQuery query)
            => Ok(await Mediator.Send(query));
    }
}