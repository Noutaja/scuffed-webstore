using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Controller.src.Controllers;
public class OrderController : BaseController<Order, IOrderService, OrderReadDTO, OrderCreateDTO, OrderUpdateDTO>
{
    private IAuthorizationService _authorizationService;
    public OrderController(IOrderService service, IAuthorizationService authorizationService) : base(service)
    {
        _authorizationService = authorizationService;
    }

    public override async Task<ActionResult<OrderReadDTO>> CreateOne([FromBody] OrderCreateDTO createObject)
    {
        return CreatedAtAction(nameof(CreateOne), await _service.CreateOneAsync(GetIdFromToken(), createObject));
    }

    public override async Task<ActionResult<OrderReadDTO>> UpdateOne([FromRoute] Guid id, [FromBody] OrderUpdateDTO updateObject)
    {
        foreach (PropertyInfo prop in updateObject.GetType().GetProperties())
        {
            Console.WriteLine(prop.Name);
            Console.WriteLine(prop.GetValue(updateObject));
        }
        OrderReadDTO? order = await _service.GetOneByIDAsync(id);
        if (order == null) return NotFound("Order not found");

        AuthorizationResult auth = await _authorizationService.AuthorizeAsync(HttpContext.User, order, "AdminOrOwner");

        if (auth.Succeeded) return await _service.UpdateOneAsync(id, updateObject);
        else if (User.Identity!.IsAuthenticated) return Forbid();
        else return Challenge();
    }

    [Authorize(Roles = "Admin")]
    public override Task<ActionResult<bool>> DeleteOne([FromRoute] Guid id)
    {
        return base.DeleteOne(id);
    }

    public override async Task<ActionResult<IEnumerable<OrderReadDTO>>> GetAll([FromQuery] GetAllParams getAllParams)
    {
        IEnumerable<OrderReadDTO> orders = await _service.GetAllAsync(getAllParams);
        if (orders.Count() < 1) return NotFound();

        AuthorizationResult auth = await _authorizationService.AuthorizeAsync(HttpContext.User, orders.First(), "AdminOrOwner");

        if (auth.Succeeded) return Ok(orders);
        else if (User.Identity!.IsAuthenticated) return Forbid();
        else return Challenge();
    }

    public override async Task<ActionResult<OrderReadDTO?>> GetOneById([FromRoute] Guid id)
    {
        OrderReadDTO? order = await _service.GetOneByIDAsync(id);
        if (order == null) return NotFound("Order not found");

        AuthorizationResult auth = await _authorizationService.AuthorizeAsync(HttpContext.User, order, "AdminOrOwner");

        if (auth.Succeeded) return Ok(order);
        else if (User.Identity!.IsAuthenticated) return Forbid();
        else return Challenge();
    }
}