using Microsoft.EntityFrameworkCore;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Framework.src.Database;

namespace ScuffedWebstore.Framework.src.Repositories;
public class OrderRepo : BaseRepo<Order>, IOrderRepo
{
    public OrderRepo(DatabaseContext database) : base(database)
    {
    }

    public override IEnumerable<Order> GetAll(GetAllParams options)
    {
        return _data.AsNoTracking().Include(o => o.OrderProducts).Skip(options.Offset).Take(options.Limit);
    }
}