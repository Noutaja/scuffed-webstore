using AutoMapper;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Shared;

namespace ScuffedWebstore.Service.src.Services;
public class AddressService : BaseService<Address, AddressReadDTO, AddressCreateDTO, AddressUpdateDTO>, IAddressService
{
    private IUserRepo _userRepo;
    public AddressService(IAddressRepo repo, IUserRepo userRepo, IMapper mapper) : base(repo, mapper)
    {
        _userRepo = userRepo;
    }

    public override async Task<AddressReadDTO> CreateOneAsync(Guid id, AddressCreateDTO createObject)
    {
        if (createObject.Street.Length < 1)
            throw CustomException.InvalidParameters("Street can't be less than 1 character long");
        if (createObject.Zipcode.Length > 10)
            throw CustomException.InvalidParameters("Zipcode can't be longer than 10 characters");
        if (createObject.City.Length < 1)
            throw CustomException.InvalidParameters("City can't be less than 1 character long");
        if (createObject.Country.Length < 1)
            throw CustomException.InvalidParameters("Country can't be less than 1 character long");

        User? u = await _userRepo.GetOneByIdAsync(id);
        if (u == null) throw CustomException.NotFoundException("User not found");

        Address a = _mapper.Map<AddressCreateDTO, Address>(createObject);
        a.UserID = id;

        return _mapper.Map<Address, AddressReadDTO>(await _repo.CreateOneAsync(a));
    }

    public override Task<AddressReadDTO> UpdateOneAsync(Guid id, AddressUpdateDTO updateObject)
    {
        if (updateObject.Street != null && updateObject.Street.Length < 1)
            throw CustomException.InvalidParameters("Street can't be less than 1 character long");
        if (updateObject.Zipcode != null && updateObject.Zipcode.Length > 10)
            throw CustomException.InvalidParameters("Zipcode can't be longer than 10 characters");
        if (updateObject.City != null && updateObject.City.Length < 1)
            throw CustomException.InvalidParameters("City can't be less than 1 character long");
        if (updateObject.Country != null && updateObject.Country.Length < 1)
            throw CustomException.InvalidParameters("Country can't be less than 1 character long");

        return base.UpdateOneAsync(id, updateObject);
    }
}