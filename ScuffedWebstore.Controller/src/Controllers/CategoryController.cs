using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Services;

namespace ScuffedWebstore.Controller.src.Controllers;
[Route("api/v1/categories")]
public class CategoryController : BaseController<Category, ICategoryService, CategoryReadDTO, CategoryCreateDTO, CategoryUpdateDTO>
{
    public CategoryController(ICategoryService service) : base(service)
    {
    }

    [AllowAnonymous]
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<CategoryReadDTO>>> GetAllAsync([FromQuery] GetAllCategoriesParams getAllParams)
    {
        return await base.GetAllAsync(getAllParams);
    }

    [AllowAnonymous]
    public override async Task<ActionResult<CategoryReadDTO?>> GetOneByIdAsync([FromRoute] Guid id)
    {
        return await base.GetOneByIdAsync(id);
    }
}