using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Controller.src.Controllers;
public class OrderController : BaseController<Order, OrderReadDTO, OrderCreateDTO, OrderUpdateDTO>
{
    public OrderController(IOrderService service) : base(service)
    {
    }
}