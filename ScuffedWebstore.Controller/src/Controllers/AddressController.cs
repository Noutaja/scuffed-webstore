using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Controller.src.Controllers;
public class AddressController : BaseController<Address, AddressReadDTO, AddressCreateDTO, AddressUpdateDTO>
{
    public AddressController(IAddressService service) : base(service)
    {
    }
}