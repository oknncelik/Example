using System.Threading.Tasks;
using Example.Business.Abstract;
using Example.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Example.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthManager _authManager;

    public AuthController(IAuthManager authManager)
    {
        _authManager = authManager;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginModel loginModel)
    {
        var userToLogin = await _authManager.Login(loginModel);
        if (!userToLogin.IsSuccess)
        {
            return BadRequest(userToLogin);
        }

        var result = await _authManager.CreateAccessToken(userToLogin);
        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterModel registerModel)
    {
        var registerResult = await _authManager.Register(registerModel);
        if (!registerResult.IsSuccess)
        {
            return BadRequest(registerResult);
        }

        var result = await _authManager.CreateAccessToken(registerResult);
        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}
