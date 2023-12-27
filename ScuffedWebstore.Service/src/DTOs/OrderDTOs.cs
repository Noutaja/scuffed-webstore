using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Types;

namespace ScuffedWebstore.Service.src.DTOs;
public class OrderReadDTO : OwnedEntity
{
    public Guid AddressID { get; set; }
    public OrderStatus Status { get; set; }
    public IEnumerable<OrderProductReadDTO> OrderProducts { get; set; }
}

public class OrderCreateDTO
{
    public Guid AddressID { get; set; }
    public IEnumerable<OrderProductCreateDTO> OrderProducts { get; set; }
}

public class OrderUpdateDTO
{
    public OrderStatus? Status { get; set; }
}