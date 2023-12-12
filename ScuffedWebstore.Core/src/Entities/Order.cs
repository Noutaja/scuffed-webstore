using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Types;

namespace ScuffedWebstore.Core.src.Entities;
public class Order : BaseEntity
{
    public Guid UserID { get; set; }
    public Guid AddressID { get; set; }
    public OrderStatus Status { get; set; }
    public IEnumerable<OrderProduct> OrderProducts { get; set; }
}
