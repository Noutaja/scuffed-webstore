using ScuffedWebstore.Core.src.Entities;

namespace ScuffedWebstore.Service.src.DTOs;
public class OrderProductReadDTO
{
    public Guid ID { get; set; }
    public Guid ProductID { get; set; }
    public Guid OrderID { get; set; }
    public int Amount { get; set; }
    public double Price { get; set; }

    public OrderProductReadDTO Convert(OrderProduct orderProduct)
    {
        return new OrderProductReadDTO
        {
            ID = orderProduct.ID,
            ProductID = orderProduct.ProductID,
            OrderID = orderProduct.OrderID,
            Amount = orderProduct.Amount,
            Price = orderProduct.Price
        };
    }
}

public class OrderProductCreateDTO
{
    public Guid ProductID { get; set; }
    public Guid OrderID { get; set; }
    public int Amount { get; set; }
    public double Price { get; set; }

    public OrderProduct Transform()
    {
        return new OrderProduct
        {
            ID = new Guid(),
            ProductID = this.ProductID,
            OrderID = this.OrderID,
            Amount = this.Amount,
            Price = this.Price
        };
    }
}