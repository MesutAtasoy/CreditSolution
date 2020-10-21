using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Api.Controllers.Base;
using User.Contract.Queries.Users.GetUserByIdentityNumber;

namespace User.Api.Controllers
{
    /// <summary>
    /// User Controller
    /// </summary>
    public class UserController : BaseController
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="mediator"></param>
        public UserController(IMediator mediator)
            : base(mediator)
        {
        }
        
        /// <summary>
        /// Returns user by identityNumber
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("ByIdentityNumber")]
        [ApiVersion("1")]
        [AllowAnonymous]
        public async Task<IActionResult> ByIdentityNumber([FromQuery] GetUserByIdentityNumberQuery query)
            => Ok(await Mediator.Send(query));
    }
}