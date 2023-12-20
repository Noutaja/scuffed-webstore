using AutoMapper;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Services;
public class AddressService : BaseService<Address, AddressReadDTO, AddressCreateFullDTO, AddressUpdateDTO>, IAddressService
{
    public AddressService(IAddressRepo repo, IMapper mapper) : base(repo, mapper)
    {
    }

    public AddressReadDTO CreateOneForProfile(Guid UserId, AddressCreateBasicDTO createObject)
    {
        AddressCreateFullDTO completeAddress = _mapper.Map<AddressCreateBasicDTO, AddressCreateFullDTO>(createObject);
        completeAddress.UserID = UserId;
        return base.CreateOne(completeAddress);
    }

    public AddressReadDTO? UpdateOneForProfile(Guid addressId, AddressUpdateDTO updateObject)
    {
        Address? a = _repo.GetOneById(addressId);
        if (a == null) return null;

        _mapper.Map(updateObject, a);
        return _mapper.Map<Address, AddressReadDTO>(_repo.UpdateOne(a));
    }
}