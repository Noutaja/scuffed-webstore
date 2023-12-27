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

    public override async Task<IEnumerable<Address>> GetAllAsync(GetAllParams options)
    {
        IQueryable<Address> results = _data.AsQueryable();

        if (options.OwnerID != null && options.OwnerID != Guid.Parse("00000000-0000-0000-0000-000000000000"))
        {
            results = results.Where(a => a.UserID == options.OwnerID);
        }

        results = results.Skip(options.Offset).Take(options.Limit);

        return await results.ToListAsync();
    }
}