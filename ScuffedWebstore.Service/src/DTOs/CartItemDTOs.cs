using ScuffedWebstore.Core.src.Entities;

namespace ScuffedWebstore.Service.src.Services;
public class CartItemReadDTO
{
    public Guid ID { get; set; }
    public Guid UserID { get; set; }
    public Guid ProductID { get; set; }
    public int Amount { get; set; }
}

public class CartItemCreateDTO
{
    public Guid UserID { get; set; }
    public Guid ProductID { get; set; }
    public int Amount { get; set; }
}

public class CartItemUpdateDTO
{
    public int Amount { get; set; }
}