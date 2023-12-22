using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
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

    [Authorize]
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<AddressReadDTO>>> GetAllAsync([FromQuery] GetAllAddressesParams getAllParams)
    {
        return await base.GetAllAsync(getAllParams);
    }

    public override async Task<ActionResult<bool>> DeleteOneAsync([FromRoute] Guid id)
    {
        return await base.DeleteOneAsync(id);
    }

    //BOTH OF THE ROUTES BELOW CAUSE SWAGGER TO ERROR AND NOT LOAD
    //All other controllers load just fine, including the same overrides
    [Authorize]
    public new async Task<ActionResult<AddressReadDTO>> CreateOneAsync([FromBody] AddressCreateDTO createObject)
    {
        return CreatedAtAction(nameof(CreateOneAsync), await _service.CreateOneAsync(GetIdFromToken(), createObject));
    }

    [Authorize]
    public override async Task<ActionResult<AddressReadDTO>> UpdateOneAsync([FromRoute] Guid id, [FromBody] AddressUpdateDTO updateObject)
    {
        /* AddressReadDTO address = await _service.GetOneByIDAsync(id);
        var authed = await _authorizationService.AuthorizeAsync(HttpContext.User, address, "AdminOrOwner"); */
        return await base.UpdateOneAsync(id, updateObject);
    }
}