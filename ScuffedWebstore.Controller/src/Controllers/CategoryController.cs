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
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<CategoryReadDTO>>> GetAll([FromQuery] GetAllCategoriesParams getAllParams)
    {
        return await base.GetAll(getAllParams);
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
}