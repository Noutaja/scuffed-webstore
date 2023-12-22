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
    public ActionResult<IEnumerable<AddressReadDTO>> GetAll([FromQuery] GetAllAddressesParams getAllParams)
    {
        return base.GetAll(getAllParams);
    }

    [Authorize]
    public new ActionResult<AddressReadDTO> CreateOne([FromBody] AddressCreateDTO createObject)
    {

        return CreatedAtAction(nameof(CreateOne), _service.CreateOne(GetIdFromToken(), createObject));
    }

    public override ActionResult<AddressReadDTO> UpdateOne([FromRoute] Guid id, [FromBody] AddressUpdateDTO updateObject)
    {
        AddressReadDTO address = _service.GetOneByID(id);
        //var authed = _authorizationService.AuthorizeAsync(HttpContext.User, address, "AdminOrOwner");
        return base.UpdateOne(id, updateObject);
    }

    public override ActionResult<bool> DeleteOne([FromRoute] Guid id)
    {
        return base.DeleteOne(id);
    }
}