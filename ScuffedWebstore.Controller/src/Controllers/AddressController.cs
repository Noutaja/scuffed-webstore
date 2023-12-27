using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Core.src.Types;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Controller.src.Controllers;
[Route("api/v1/addresses")]
public class AddressController : BaseController<Address, IAddressService, AddressReadDTO, AddressCreateDTO, AddressUpdateDTO>
{
    private IAuthorizationService _authorizationService;
    public AddressController(IAddressService service, IAuthorizationService authorizationService) : base(service)
    {
        _authorizationService = authorizationService;
    }

    public override async Task<ActionResult<IEnumerable<AddressReadDTO>>> GetAll([FromQuery] GetAllParams getAllParams)
    {
        ClaimsPrincipal claims = HttpContext.User;
        string userRole = claims.FindFirst(c => c.Type == ClaimTypes.Role)!.Value;

        if (getAllParams.OwnerID != null)
        {
            if (getAllParams.OwnerID != GetIdFromToken() || userRole != "Admin") return Forbid();
        }
        else
        {
            if (userRole != "Admin") return Forbid();
        }

        return await base.GetAll(getAllParams);
    }

    public override async Task<ActionResult<AddressReadDTO?>> GetOneById([FromRoute] Guid id)
    {
        AddressReadDTO? address = await _service.GetOneByIDAsync(id);
        if (address == null) return NotFound("Address not found");

        AuthorizationResult auth = await _authorizationService.AuthorizeAsync(HttpContext.User, address, "AdminOrOwner");

        if (auth.Succeeded) return address;
        else if (User.Identity!.IsAuthenticated) return Forbid();
        else return Challenge();
    }

    [Authorize]
    public override async Task<ActionResult<bool>> DeleteOne([FromRoute] Guid id)
    {
        AddressReadDTO? address = await _service.GetOneByIDAsync(id);
        if (address == null) return NotFound("Address not found");

        Console.WriteLine(address.UserID);
        AuthorizationResult auth = await _authorizationService.AuthorizeAsync(HttpContext.User, address, "AdminOrOwner");
        Console.WriteLine(auth.Succeeded);

        if (auth.Succeeded) return await _service.DeleteOneAsync(id);
        else if (User.Identity!.IsAuthenticated) return Forbid();
        else return Challenge();
    }

    public override async Task<ActionResult<AddressReadDTO>> UpdateOne([FromRoute] Guid id, [FromBody] AddressUpdateDTO updateObject)
    {
        AddressReadDTO? address = await _service.GetOneByIDAsync(id);
        if (address == null) return NotFound("Address not found");

        AuthorizationResult auth = await _authorizationService.AuthorizeAsync(HttpContext.User, address, "AdminOrOwner");

        if (auth.Succeeded) return await _service.UpdateOneAsync(id, updateObject);
        else if (User.Identity!.IsAuthenticated) return Forbid();
        else return Challenge();
    }

    public override async Task<ActionResult<AddressReadDTO>> CreateOne([FromBody] AddressCreateDTO createObject)
    {
        return CreatedAtAction(nameof(CreateOne), await _service.CreateOneAsync(GetIdFromToken(), createObject));
    }
}