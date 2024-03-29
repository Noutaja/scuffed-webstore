using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Shared;

namespace ScuffedWebstore.Controller.src.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponse>> LoginAsync([FromBody] Credentials credentials)
    {
        string token = await _authService.LoginAsync(credentials.Email, credentials.Password);
        if (string.IsNullOrEmpty(token)) return Forbid();

        return Ok(new LoginResponse { accessToken = token });
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<ActionResult<UserReadDTO>> GetProfileAsync()
    {
        return Ok(await _authService.GetProfileAsync(GetIdFromToken()));
    }

    [HttpDelete("profile/delete")]
    [Authorize]
    public async Task<ActionResult<bool>> DeleteProfileAsync()
    {
        await _authService.DeleteProfileAsync(GetIdFromToken());
        return NoContent();
    }

    [HttpPatch("profile/password")]
    [Authorize]
    public async Task<ActionResult<bool>> ChangePasswordAsync([FromBody] string password)
    {
        return Ok(await _authService.ChangePasswordAsync(GetIdFromToken(), password));
    }

    private Guid GetIdFromToken()
    {
        ClaimsPrincipal claims = HttpContext.User;
        string id = claims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        Guid guid = new Guid(id);
        return guid;
    }
}

public struct LoginResponse()
{
    public string accessToken { get; set; }
}
