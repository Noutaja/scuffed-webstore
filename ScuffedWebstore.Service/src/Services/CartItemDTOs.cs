using ScuffedWebstore.Core.src.Entities;

namespace ScuffedWebstore.Service.src.Services;
public class CartItemReadDTO
{
    public Guid ID { get; set; }
    public Guid UserID { get; set; }
    public Guid ProductID { get; set; }
    public int Amount { get; set; }

    public CartItemReadDTO Convert(CartItem cartItem)
    {
        return new CartItemReadDTO
        {
            ID = cartItem.ID,
            UserID = cartItem.UserID,
            ProductID = cartItem.ProductID,
            Amount = cartItem.Amount
        };
    }
}

public class CartItemCreateDTO
{
    public Guid UserID { get; set; }
    public Guid ProductID { get; set; }
    public int Amount { get; set; }

    public CartItem Transform()
    {
        return new CartItem
        {
            ID = new Guid(),
            UserID = this.UserID,
            ProductID = this.ProductID,
            Amount = this.Amount
        };
    }
}

public class CartItemUpdateDTO
{
    public int Amount { get; set; }

    public CartItem ApplyTo(CartItem cartItem)
    {
        cartItem.Amount = this.Amount;
        return cartItem;
    }
}