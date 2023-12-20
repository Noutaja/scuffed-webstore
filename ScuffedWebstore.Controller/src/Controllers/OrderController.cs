using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Services;

namespace ScuffedWebstore.Controller.src.Controllers;
public class OrderController : BaseController<Order, OrderService, OrderReadDTO, OrderCreateDTO, OrderUpdateDTO>
{
    public OrderController(OrderService service) : base(service)
    {
    }

    [Authorize]
    public override ActionResult<OrderReadDTO> CreateOne([FromBody] OrderCreateDTO createObject)
    {
        return base.CreateOne(createObject);
    }
}