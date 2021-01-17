using Example.Business.Abstract;
using Example.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Example.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginModel login)
        {
            var user = await _authManager.Login(login);
            if (!user.IsSuccess)
                return BadRequest(user.Message);

            var result = await _authManager.CreateAccessToken(user.Result);
            if (result.IsSuccess)
                return Ok(result.Result);

            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel register)
        {
            return Ok(await _authManager.Register(register));
        }
    }
}
