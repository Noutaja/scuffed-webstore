using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Core.src.Types;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Controller.src.Controllers;

public class UserController : BaseController<User, IUserService, UserReadDTO, UserCreateDTO, UserUpdateDTO>
{
    public UserController(IUserService service) : base(service)
    {
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<UserReadDTO>>> GetAllAsync([FromQuery] GetAllUsersParams getAllParams)
    {
        return await base.GetAllAsync(getAllParams);
    }

    [AllowAnonymous]
    public override async Task<ActionResult<UserReadDTO>> CreateOneAsync([FromBody] UserCreateDTO createObject)
    {
        return await base.CreateOneAsync(createObject);
    }

    [HttpPatch("role/{id:guid}")]
    public async Task<ActionResult<UserReadDTO>> UpdateRole([FromRoute] Guid id, [FromQuery] UserRole userRole)
    {
        return await _service.UpdateRoleAsync(id, userRole);
    }
}