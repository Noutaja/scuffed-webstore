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

    public override IEnumerable<Address> GetAll(GetAllParams options)
    {
        return _data.AsNoTracking().Skip(options.Offset).Take(options.Limit);
    }
}