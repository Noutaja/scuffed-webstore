using AutoMapper;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Services;
public class OrderService : BaseService<Order, OrderReadDTO, OrderCreateDTO, OrderUpdateDTO>, IOrderService
{
    public OrderService(IBaseRepo<Order> repo, IMapper mapper) : base(repo, mapper)
    {
    }
}