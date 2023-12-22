using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Types;

namespace ScuffedWebstore.Core.src.Entities;
public class Order : EntityWithOwner
{
    public Guid AddressID { get; set; }
    public OrderStatus Status { get; set; }
    public IEnumerable<OrderProduct> OrderProducts { get; set; }
}
