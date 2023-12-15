using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Services;
public class AddressService : IAddressService
{
    public AddressReadDTO CreateOne(AddressCreateDTO address)
    {
        throw new NotImplementedException();
    }

    public bool DeleteOne(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<AddressReadDTO> GetAll(GetAllParams options)
    {
        throw new NotImplementedException();
    }

    public AddressReadDTO? GetOneById(Guid id)
    {
        throw new NotImplementedException();
    }

    public AddressReadDTO UpdateOne(Guid id, AddressUpdateDTO address)
    {
        throw new NotImplementedException();
    }
}