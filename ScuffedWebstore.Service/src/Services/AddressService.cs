using AutoMapper;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
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

    public AddressReadDTO? UpdateOneForProfile(Guid id, AddressUpdateDTO updateObject)
    {
        Address? a = _repo.GetOneById(id);
        if (a == null) return null;

        _mapper.Map(updateObject, a);
        return _mapper.Map<Address, AddressReadDTO>(_repo.UpdateOne(a));
    }

    public bool DeleteOneFromProfile(Guid id)
    {
        Address? a = _repo.GetOneById(id);
        if (a == null) return false;

        return _repo.DeleteOne(id);
    }

    public IEnumerable<AddressReadDTO> GetAllForProfile(Guid userId)
    {
        return _mapper.Map<IEnumerable<Address>, IEnumerable<AddressReadDTO>>(_repo.GetAll(new GetAllParams { id = userId }));
    }
}