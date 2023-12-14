using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Service.src.Abstractions;
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
    public ActionResult<string> Login([FromBody] Credentials credentials)
    {
        string token = _authService.Login(credentials.email, credentials.password);
        if (string.IsNullOrEmpty(token)) return Forbid();

        return Ok(token);
    }

}
