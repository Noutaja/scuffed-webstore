using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Controller.src.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]s")]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet()]
        public ActionResult<IEnumerable<UserReadDTO>> GetAll([FromQuery] GetAllParams options)
        {
            return Ok(_userService.GetAll(options));
        }

        [HttpPost()]
        public ActionResult<UserReadDTO> CreateOne([FromBody] UserCreateDTO userCreateDto)
        {
            return CreatedAtAction(nameof(CreateOne), _userService.CreateOne(userCreateDto));
        }

        [HttpPatch()]
        public ActionResult<UserReadDTO> UpdateOne([FromQuery] Guid id, [FromBody] UserUpdateDTO userUpdateDTO)
        {
            return Ok(_userService.UpdateOne(id, userUpdateDTO));
        }

        [HttpDelete()]
        public ActionResult<bool> DeleteOne([FromQuery] Guid id)
        {
            _userService.DeleteOne(id);
            return NoContent();
        }
    }
}