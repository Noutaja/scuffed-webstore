using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Types;

namespace ScuffedWebstore.Service.src.DTOs;
public class OrderReadDTO
{
    public Guid ID { get; set; }
    public Guid UserID { get; set; }
    public Guid AddressID { get; set; }
    public OrderStatus Status { get; set; }
    public IEnumerable<OrderProduct> OrderProducts { get; set; }

    public OrderReadDTO Convert(Order order)
    {
        return new OrderReadDTO
        {
            ID = order.ID,
            UserID = order.UserID,
            AddressID = order.AddressID,
            Status = order.Status,
            OrderProducts = order.OrderProducts
        };
    }
}

public class OrderCreateDTO
{
    public Guid UserID { get; set; }
    public Guid AddressID { get; set; }
    public OrderStatus Status { get; set; }
    public IEnumerable<OrderProduct> OrderProducts { get; set; }

    public Order Transform()
    {
        return new Order
        {
            ID = new Guid(),
            UserID = this.UserID,
            AddressID = this.AddressID,
            Status = this.Status,
            OrderProducts = this.OrderProducts
        };
    }
}

public class OrderUpdateDTO
{
    public OrderStatus? Status { get; set; }

    public Order ApplyTo(Order order)
    {
        if (this.Status != null) order.Status = (OrderStatus)this.Status;
        return order;
    }
}