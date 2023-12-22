using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Controller.src.Controllers;
public class OrderController : BaseController<Order, IOrderService, OrderReadDTO, OrderCreateDTO, OrderUpdateDTO>
{
    public OrderController(IOrderService service) : base(service)
    {
    }

    [Authorize]
    public override async Task<ActionResult<OrderReadDTO>> CreateOneAsync([FromBody] OrderCreateDTO createObject)
    {
        return CreatedAtAction(nameof(CreateOneAsync), await _service.CreateOneAsync(GetIdFromToken(), createObject));
    }
}