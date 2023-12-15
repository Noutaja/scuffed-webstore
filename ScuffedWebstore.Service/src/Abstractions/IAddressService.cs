using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Abstractions;
public interface IAddressService
{
    public AddressReadDTO CreateOne(AddressCreateDTO address);
    public bool DeleteOne(Guid id);
    public IEnumerable<AddressReadDTO> GetAll(GetAllParams options);
    public AddressReadDTO? GetOneById(Guid id);
    public AddressReadDTO UpdateOne(Guid id, AddressUpdateDTO address);

}
