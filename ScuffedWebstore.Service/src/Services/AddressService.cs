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

    public IEnumerable<AddressReadDTO> GetAllForProfile(Guid userId)
    {
        return _mapper.Map<IEnumerable<Address>, IEnumerable<AddressReadDTO>>(_repo.GetAll(new GetAllAddressesParams { UserID = userId }));
    }
}