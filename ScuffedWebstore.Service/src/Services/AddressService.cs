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
        User? u = await _userRepo.GetOneByIdAsync(id);
        if (u == null) throw CustomException.NotFoundException("User not found");

        Address a = _mapper.Map<AddressCreateDTO, Address>(createObject);
        a.UserID = id;

        return _mapper.Map<Address, AddressReadDTO>(await _repo.CreateOneAsync(a));
    }

    /* public async Task<IEnumerable<AddressReadDTO>> GetAllForProfileAsync(Guid userId)
    {
        return _mapper.Map<IEnumerable<Address>, IEnumerable<AddressReadDTO>>(await _repo.GetAllAsync(new GetAllAddressesParams { UserID = userId }));
    } */
}