using ScuffedWebstore.Core.src.Abstractions;

namespace ScuffedWebstore.Core.src.Entities;
public class CartItem : BaseEntity
{
    public Guid UserID { get; set; }
    public Guid ProductID { get; set; }
    public int Amount { get; set; }
}