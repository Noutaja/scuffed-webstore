using ScuffedWebstore.Core.src.Abstractions;

namespace ScuffedWebstore.Core.src.Entities;
public class OrderProduct : BaseEntity
{
    public Guid ProductID { get; set; }
    public Guid OrderID { get; set; }
    public int Amount { get; set; }
    public double Price { get; set; }
}