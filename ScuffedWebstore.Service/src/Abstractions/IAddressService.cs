using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Abstractions;
public interface IAddressService : IBaseService<Address, AddressReadDTO, AddressCreateFullDTO, AddressUpdateDTO>
{
    public AddressReadDTO CreateOneForProfile(Guid UserId, AddressCreateBasicDTO createObject);
    public AddressReadDTO? UpdateOneForProfile(Guid addressId, AddressUpdateDTO updateObject);
}
