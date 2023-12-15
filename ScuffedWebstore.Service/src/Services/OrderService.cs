using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Services;
public class OrderService : IOrderService
{
    public OrderReadDTO CreateOne(OrderCreateDTO address)
    {
        throw new NotImplementedException();
    }

    public bool DeleteOne(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<OrderReadDTO> GetAll(GetAllParams options)
    {
        throw new NotImplementedException();
    }

    public OrderReadDTO? GetOneById(Guid id)
    {
        throw new NotImplementedException();
    }

    public OrderReadDTO UpdateOne(Guid id, OrderUpdateDTO address)
    {
        throw new NotImplementedException();
    }
}