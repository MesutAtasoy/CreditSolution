using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CreditScore.Api.Controllers.Base
{
    /// <summary>
    /// Base Controller
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BaseController : ControllerBase
    {
        /// <summary>
        /// Mediator
        /// </summary>
        protected readonly IMediator Mediator;
        
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediator"></param>
        public BaseController(IMediator mediator)
        {
            Mediator = mediator;
        }
    }
}