using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Controller.src.Controllers;

//[Authorize(Roles = "Admin")]
public class UserController : BaseController<User, UserReadDTO, UserCreateDTO, UserUpdateDTO>
{
    public UserController(IUserService service) : base(service)
    {
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