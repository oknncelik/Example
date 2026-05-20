using System.Threading.Tasks;
using Example.Business.Abstract;
using Example.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Example.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserOperationClaimsController : ControllerBase
{
    private readonly IUserOperationClaimManager _userOperationClaimManager;

    public UserOperationClaimsController(IUserOperationClaimManager userOperationClaimManager)
    {
        _userOperationClaimManager = userOperationClaimManager;
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _userOperationClaimManager.GetList();
        if (result.IsSuccess) return Ok(result);
        return BadRequest(result);
    }

    [HttpGet("getbyuserid")]
    public async Task<IActionResult> GetByUserId(int userId)
    {
        var result = await _userOperationClaimManager.GetByUserId(userId);
        if (result.IsSuccess) return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("add")]
    public async Task<IActionResult> Add(UserOperationClaimModel userOperationClaim)
    {
        var result = await _userOperationClaimManager.Add(userOperationClaim);
        if (result.IsSuccess) return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update(UserOperationClaimModel userOperationClaim)
    {
        var result = await _userOperationClaimManager.Update(userOperationClaim);
        if (result.IsSuccess) return Ok(result);
        return BadRequest(result);
    }

    [HttpPost("delete")]
    public async Task<IActionResult> Delete(UserOperationClaimModel userOperationClaim)
    {
        var result = await _userOperationClaimManager.Delete(userOperationClaim);
        if (result.IsSuccess) return Ok(result);
        return BadRequest(result);
    }
}
