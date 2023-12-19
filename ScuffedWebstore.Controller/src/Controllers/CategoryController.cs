using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Controller.src.Controllers;
public class CategoryController : BaseController<Category, CategoryReadDTO, CategoryCreateDTO, CategoryUpdateDTO>
{
    public CategoryController(ICategoryService service) : base(service)
    {
    }
}