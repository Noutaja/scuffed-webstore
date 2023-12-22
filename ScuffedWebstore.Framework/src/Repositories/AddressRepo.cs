using Microsoft.EntityFrameworkCore;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Framework.src.Database;

namespace ScuffedWebstore.Framework.src.Repositories;
public class AddressRepo : BaseRepo<Address>, IAddressRepo
{

    public AddressRepo(DatabaseContext database) : base(database)
    { }

    public async Task<IEnumerable<Address>> GetAllAsync(GetAllAddressesParams options)
    {
        List<Address> data;
        if (options.UserID != null) data = await _data.AsNoTracking().Where(a => a.UserID == options.UserID).Skip(options.Offset).Take(options.Limit).ToListAsync();
        else data = await _data.AsNoTracking().Skip(options.Offset).Take(options.Limit).ToListAsync();
        return data;
    }

    public override async Task<IEnumerable<Address>> GetAllAsync(GetAllParams options)
    {
        return await _data.AsNoTracking().Skip(options.Offset).Take(options.Limit).ToListAsync();
    }
}