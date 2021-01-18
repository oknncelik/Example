using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Example.Api.Controllers
{
    [Authorize]
    [Route("api/sample")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(new { Status = "Ok." });
        }
    }
}
