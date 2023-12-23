using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Controller.src.Controllers;
[Route("api/v1/categories")]
public class CategoryController : BaseController<Category, ICategoryService, CategoryReadDTO, CategoryCreateDTO, CategoryUpdateDTO>
{
    public CategoryController(ICategoryService service) : base(service)
    {
    }

    [AllowAnonymous]
    public override Task<ActionResult<IEnumerable<CategoryReadDTO>>> GetAll([FromQuery] GetAllParams getAllParams)
    {
        return base.GetAll(getAllParams);
    }

    [AllowAnonymous]
    public override async Task<ActionResult<CategoryReadDTO?>> GetOneById([FromRoute] Guid id)
    {
        return await base.GetOneById(id);
    }

    [Authorize(Roles = "Admin")]
    public override Task<ActionResult<CategoryReadDTO>> CreateOne([FromBody] CategoryCreateDTO createObject)
    {
        return base.CreateOne(createObject);
    }

    [Authorize(Roles = "Admin")]
    public override Task<ActionResult<bool>> DeleteOne([FromRoute] Guid id)
    {
        return base.DeleteOne(id);
    }

    [Authorize(Roles = "Admin")]
    public override Task<ActionResult<CategoryReadDTO>> UpdateOne([FromRoute] Guid id, [FromBody] CategoryUpdateDTO updateObject)
    {
        return base.UpdateOne(id, updateObject);
    }
}