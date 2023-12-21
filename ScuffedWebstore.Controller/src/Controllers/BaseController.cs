using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.Abstractions;

namespace ScuffedWebstore.Controller.src.Controllers;
[ApiController]
[Route("api/v1/[controller]s")]
[Authorize(Roles = "Admin")]
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
    public virtual ActionResult<TReadDTO> CreateOne([FromBody] TCreateDTO createObject)
    {
        return CreatedAtAction(nameof(CreateOne), _service.CreateOne(createObject));
    }

    [HttpDelete("{id:guid}")]
    public virtual ActionResult<bool> DeleteOne([FromRoute] Guid id)
    {
        _service.DeleteOne(id);
        return NoContent();
    }

    [HttpGet()]
    public virtual ActionResult<IEnumerable<TReadDTO>> GetAll([FromQuery] GetAllParams getAllParams)
    {
        return Ok(_service.GetAll(getAllParams));
    }

    [HttpGet("{id:guid}")]
    public virtual ActionResult<TReadDTO>? GetOneById([FromRoute] Guid id)
    {
        return Ok(_service.GetOneById(id));
    }

    [HttpPatch("{id:guid}")]
    public virtual ActionResult<TReadDTO> UpdateOne([FromRoute] Guid id, [FromBody] TUpdateDTO updateObject)
    {
        return Ok(_service.UpdateOne(id, updateObject));
    }
}