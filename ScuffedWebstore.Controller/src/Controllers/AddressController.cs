using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Services;

namespace ScuffedWebstore.Controller.src.Controllers;
[Route("api/v1/addresses")]
public class AddressController : BaseController<Address, AddressService, AddressReadDTO, AddressCreateFullDTO, AddressUpdateDTO>
{
    public AddressController(AddressService service) : base(service)
    {
    }

    public override ActionResult<AddressReadDTO> CreateOne([FromBody] AddressCreateFullDTO createObject)
    {
        return base.CreateOne(createObject);
    }

    [HttpPost("profile")]
    [Authorize]
    public ActionResult<AddressReadDTO> CreateOneForProfile([FromBody] AddressCreateBasicDTO createObject)
    {
        return _service.CreateOneForProfile(GetIdFromToken(), createObject);
    }

    [Authorize]
    public override ActionResult<AddressReadDTO> UpdateOne([FromRoute] Guid id, [FromBody] AddressUpdateDTO updateObject)
    {
        return base.UpdateOne(id, updateObject);
    }

    [HttpPatch("profile/{id:guid}")]
    [Authorize]
    public ActionResult<AddressReadDTO> UpdateOneForProfile([FromRoute] Guid id, [FromBody] AddressUpdateDTO updateObject)
    {
        AddressReadDTO? a = _service.GetOneById(id);
        if (a == null) NotFound();

        Guid userID = GetIdFromToken();
        if (a.UserID != userID) return Forbid();

        return _service.UpdateOneForProfile(id, updateObject);
    }

    [Authorize]
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