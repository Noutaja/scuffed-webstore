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
    public override ActionResult<OrderReadDTO> CreateOne([FromBody] OrderCreateDTO createObject)
    {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        Guid g = new Guid(userId);
        if (g != createObject.UserID) Unauthorized("Not authorized");

        return CreatedAtAction(nameof(CreateOne), _service.CreateOne(createObject));
    }
}