using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.Abstractions;

namespace ScuffedWebstore.Controller.src.Controllers;
[ApiController]
[Route("api/v1/[controller]s")]
//[Authorize(Roles = "Admin")]
public class BaseController<T, TService, TReadDTO, TCreateDTO, TUpdateDTO> : ControllerBase
    where T : BaseEntity
    where TService : IBaseService<T, TReadDTO, TCreateDTO, TUpdateDTO>
{
    protected TService _service;

    public BaseController(TService service)
    {
        _service = service;
    }

    [HttpPost()]
    public virtual async Task<ActionResult<TReadDTO>> CreateOneAsync([FromBody] TCreateDTO createObject)
    {
        return CreatedAtAction(nameof(CreateOneAsync), await _service.CreateOneAsync(new Guid(), createObject));
    }

    [HttpDelete("{id:guid}")]
    public virtual async Task<ActionResult<bool>> DeleteOneAsync([FromRoute] Guid id)
    {
        await _service.DeleteOneAsync(id);
        return NoContent();
    }

    [HttpGet()]
    public virtual async Task<ActionResult<IEnumerable<TReadDTO>>> GetAllAsync([FromQuery] GetAllParams getAllParams)
    {
        return Ok(await _service.GetAllAsync(getAllParams));
    }

    [HttpGet("{id:guid}")]
    public virtual async Task<ActionResult<TReadDTO?>> GetOneByIdAsync([FromRoute] Guid id)
    {
        return Ok(await _service.GetOneByIDAsync(id));
    }

    [HttpPatch("{id:guid}")]
    public virtual async Task<ActionResult<TReadDTO>> UpdateOneAsync([FromRoute] Guid id, [FromBody] TUpdateDTO updateObject)
    {
        return Ok(await _service.UpdateOneAsync(id, updateObject));
    }

    protected Guid GetIdFromToken()
    {
        ClaimsPrincipal claims = HttpContext.User;
        string id = claims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value;
        Guid guid = new Guid(id);
        return guid;
    }
}