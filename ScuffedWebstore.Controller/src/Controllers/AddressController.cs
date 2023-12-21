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
    public AddressController(IAddressService service) : base(service)
    {
    }

    [Authorize]
    [HttpGet("search")]
    public ActionResult<IEnumerable<AddressReadDTO>> GetAll([FromQuery] GetAllAddressesParams getAllParams)
    {
        return base.GetAll(getAllParams);
    }

    public override ActionResult<AddressReadDTO> CreateOne([FromBody] AddressCreateDTO createObject)
    {
        return base.CreateOne(createObject);
    }

    public override ActionResult<AddressReadDTO> UpdateOne([FromRoute] Guid id, [FromBody] AddressUpdateDTO updateObject)
    {
        return base.UpdateOne(id, updateObject);
    }

    public override ActionResult<bool> DeleteOne([FromRoute] Guid id)
    {
        return base.DeleteOne(id);
    }

    private Guid GetIdFromToken()
    {
        ClaimsPrincipal claims = HttpContext.User;
        string id = claims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        Guid guid = new Guid(id);
        return guid;
    }
}