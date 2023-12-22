using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;

namespace ScuffedWebstore.Core.src.Abstractions;
public interface IAddressRepo : IBaseRepo<Address>
{
    public Task<IEnumerable<Address>> GetAllAsync(GetAllAddressesParams options);
}