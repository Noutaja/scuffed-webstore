using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Abstractions;
public interface IOrderService
{
    public OrderReadDTO CreateOne(OrderCreateDTO address);
    public bool DeleteOne(Guid id);
    public IEnumerable<OrderReadDTO> GetAll(GetAllParams options);
    public OrderReadDTO? GetOneById(Guid id);
    public OrderReadDTO UpdateOne(Guid id, OrderUpdateDTO address);
}