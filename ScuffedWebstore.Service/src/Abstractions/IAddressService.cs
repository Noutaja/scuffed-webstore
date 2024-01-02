using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Abstractions;
public interface IAddressService : IBaseService<Address, AddressReadDTO, AddressCreateDTO, AddressUpdateDTO>
{

}
