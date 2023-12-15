using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
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
        Request.Headers.TryGetValue("Authorization", out var token);
        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        JwtSecurityToken? jwtToken = handler.ReadJwtToken(token.ToString().Replace("Bearer ", string.Empty));
        var id = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid").Value;
        return _authService.GetProfile(id);
    }
}
