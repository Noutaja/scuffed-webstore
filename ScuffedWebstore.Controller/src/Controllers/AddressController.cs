using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Services;

namespace ScuffedWebstore.Controller.src.Controllers;
[Route("api/v1/addresses")]
public class AddressController : BaseController<Address, IAddressService, AddressReadDTO, AddressCreateFullDTO, AddressUpdateDTO>
{
    public AddressController(IAddressService service) : base(service)
    {
    }

    public override ActionResult<AddressReadDTO> CreateOne([FromBody] AddressCreateFullDTO createObject)
    {
        return base.CreateOne(createObject);
    }

    [HttpGet("profile")]
    [Authorize]
    public ActionResult<IEnumerable<AddressReadDTO>> GetAllForProfile()
    {
        return Ok(_service.GetAllForProfile(GetIdFromToken()));
    }

    [HttpPost("profile")]
    [Authorize]
    public ActionResult<AddressReadDTO> CreateOneForProfile([FromBody] AddressCreateBasicDTO createObject)
    {
        return CreatedAtAction(nameof(CreateOneForProfile), _service.CreateOneForProfile(GetIdFromToken(), createObject));
    }

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

        return Ok(_service.UpdateOneForProfile(id, updateObject));
    }

    public override ActionResult<bool> DeleteOne([FromRoute] Guid id)
    {
        return base.DeleteOne(id);
    }

    [HttpDelete("profile")]
    [Authorize]
    public ActionResult<bool> DeleteOneFromProfile()
    {
        _service.DeleteOneFromProfile(GetIdFromToken());
        return NoContent();
    }

    private Guid GetIdFromToken()
    {
        ClaimsPrincipal claims = HttpContext.User;
        string id = claims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        Guid guid = new Guid(id);
        return guid;
    }
}