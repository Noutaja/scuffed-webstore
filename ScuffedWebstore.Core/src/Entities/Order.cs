using System.ComponentModel.DataAnnotations.Schema;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Types;

namespace ScuffedWebstore.Core.src.Entities;
public class Order : OwnedEntity
{
    [ForeignKey("Address")] public Guid AddressID { get; set; }
    public virtual Address Address { get; set; }
    public OrderStatus Status { get; set; }
    public IEnumerable<OrderProduct> OrderProducts { get; set; }
}
