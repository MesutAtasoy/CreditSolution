using Microsoft.AspNetCore.Mvc;

namespace CreditScore.Api.Controllers
{
    /// <summary>
    /// Ping
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PingController : ControllerBase
    {
        /// <summary>
        /// Ping Pong Ping
        /// </summary>
        /// <returns></returns>
        [HttpGet("Ping")]
        public IActionResult Get()
        {
            return Ok("Pong");
        }
    }
}