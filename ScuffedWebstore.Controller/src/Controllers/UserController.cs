using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Types;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Services;

namespace ScuffedWebstore.Controller.src.Controllers;

public class UserController : BaseController<User, UserService, UserReadDTO, UserCreateDTO, UserUpdateDTO>
{
    public UserController(UserService service) : base(service)
    {
    }

    [AllowAnonymous]
    public override ActionResult<UserReadDTO> CreateOne([FromBody] UserCreateDTO createObject)
    {
        return base.CreateOne(createObject);
    }

    [HttpPatch("/role/{id:guid}")]
    public ActionResult<UserReadDTO> UpdateRole([FromRoute] Guid id, [FromBody] UserRole userRole)
    {
        return _service.UpdateRole(id, userRole);
    }

    /* [HttpGet()]
    public ActionResult<IEnumerable<UserReadDTO>> GetAll([FromQuery] GetAllParams options)
    {
        return Ok(_userService.GetAll(options));
    }

    [HttpGet("{id}")]
    public ActionResult<UserReadDTO> GetOneById([FromRoute] Guid id)
    {
        return Ok(_userService.GetOneById(id));
    }

    [HttpPost()]
    [AllowAnonymous]
    public ActionResult<UserReadDTO> CreateOne([FromBody] UserCreateDTO userCreateDto)
    {
        return CreatedAtAction(nameof(CreateOne), _userService.CreateOne(userCreateDto));
    }

    [HttpPatch("{id}")]
    public ActionResult<UserReadDTO> UpdateOne([FromRoute] Guid id, [FromBody] UserUpdateDTO userUpdateDTO)
    {
        return Ok(_userService.UpdateOne(id, userUpdateDTO));
    }

    [HttpDelete("{id}")]
    public ActionResult<bool> DeleteOne([FromRoute] Guid id)
    {
        _userService.DeleteOne(id);
        return NoContent();
    } */

}