using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Core.src.Types;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Services;

namespace ScuffedWebstore.Controller.src.Controllers;

public class UserController : BaseController<User, IUserService, UserReadDTO, UserCreateDTO, UserUpdateDTO>
{
    public UserController(IUserService service) : base(service)
    {
    }

    [HttpGet("search")]
    public ActionResult<IEnumerable<UserReadDTO>> GetAll([FromQuery] GetAllUsersParams getAllParams)
    {
        return base.GetAll(getAllParams);
    }

    [AllowAnonymous]
    public override ActionResult<UserReadDTO> CreateOne([FromBody] UserCreateDTO createObject)
    {
        return base.CreateOne(createObject);
    }

    [HttpPatch("role/{id:guid}")]
    public ActionResult<UserReadDTO> UpdateRole([FromRoute] Guid id, [FromBody] UserRole userRole)
    {
        return _service.UpdateRole(id, userRole);
    }
}