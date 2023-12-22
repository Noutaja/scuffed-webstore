using AutoMapper;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Services;
public class AddressService : BaseService<Address, AddressReadDTO, AddressCreateDTO, AddressUpdateDTO>, IAddressService
{
    public AddressService(IAddressRepo repo, IMapper mapper) : base(repo, mapper)
    {
    }

    public async Task<IEnumerable<AddressReadDTO>> GetAllForProfileAsync(Guid userId)
    {
        return _mapper.Map<IEnumerable<Address>, IEnumerable<AddressReadDTO>>(await _repo.GetAllAsync(new GetAllAddressesParams { UserID = userId }));
    }
}