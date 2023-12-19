using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Controller.src.Controllers;
public class AddressController : BaseController<Address, AddressReadDTO, AddressCreateDTO, AddressUpdateDTO>
{
    public AddressController(IAddressService service) : base(service)
    {
    }

    [Authorize]
    public override ActionResult<AddressReadDTO> CreateOne([FromBody] AddressCreateDTO createObject)
    {
        return base.CreateOne(createObject);
    }

    [Authorize]
    public override ActionResult<AddressReadDTO> UpdateOne([FromRoute] Guid id, [FromBody] AddressUpdateDTO updateObject)
    {
        return base.UpdateOne(id, updateObject);
    }

    [Authorize]
    public override ActionResult<bool> DeleteOne([FromRoute] Guid id)
    {
        return base.DeleteOne(id);
    }
}