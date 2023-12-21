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
    public ActionResult<string> Login([FromBody] Credentials credentials)
    {
        string token = _authService.Login(credentials.email, credentials.password);
        if (string.IsNullOrEmpty(token)) return Forbid();

        return Ok(token);
    }

    [HttpGet("profile")]
    [Authorize]
    public ActionResult<UserReadDTO> GetProfile()
    {
        return Ok(_authService.GetProfile(GetIdFromToken()));
    }

    [HttpDelete("profile/delete")]
    [Authorize]
    public ActionResult<bool> DeleteProfile()
    {
        if (!_authService.DeleteProfile(GetIdFromToken())) return Unauthorized("Not authorized");
        return NoContent();
    }

    [HttpPatch("profile/password")]
    [Authorize]
    public ActionResult<bool> ChangePassword([FromBody] string password)
    {
        return Ok(_authService.ChangePassword(GetIdFromToken(), password));
    }

    private Guid GetIdFromToken()
    {
        ClaimsPrincipal claims = HttpContext.User;
        string id = claims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        Guid guid = new Guid(id);
        return guid;
    }
}
