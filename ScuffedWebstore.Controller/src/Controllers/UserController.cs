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
    private IAuthorizationService _authorizationService;
    public UserController(IUserService service, IAuthorizationService authorizationService) : base(service)
    {
        _authorizationService = authorizationService;
    }

    [AllowAnonymous]
    public override async Task<ActionResult<UserReadDTO>> CreateOne([FromBody] UserCreateDTO createObject)
    {
        return await base.CreateOne(createObject);
    }

    [HttpPatch("role/{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<UserReadDTO>> UpdateRole([FromRoute] Guid id, [FromQuery] UserRole userRole)
    {
        return await _service.UpdateRoleAsync(id, userRole);
    }

    public override async Task<ActionResult<UserReadDTO>> UpdateOne([FromRoute] Guid id, [FromBody] UserUpdateDTO updateObject)
    {
        UserReadDTO? order = await _service.GetOneByIDAsync(id);
        if (order == null) return NotFound("Order not found");

        AuthorizationResult auth = await _authorizationService.AuthorizeAsync(HttpContext.User, order, "AdminOrOwner");

        if (auth.Succeeded) return order;
        else if (User.Identity!.IsAuthenticated) return Forbid();
        else return Challenge();
    }

    [Authorize(Roles = "Admin")]
    public override Task<ActionResult<bool>> DeleteOne([FromRoute] Guid id)
    {
        return base.DeleteOne(id);
    }

    [Authorize(Roles = "Admin")]
    public override async Task<ActionResult<IEnumerable<UserReadDTO>>> GetAll([FromQuery] GetAllParams getAllParams)
    {
        return await base.GetAll(getAllParams);
    }

    [Authorize(Roles = "Admin")]
    public override async Task<ActionResult<UserReadDTO?>> GetOneById([FromRoute] Guid id)
    {
        return await base.GetOneById(id);
    }
}