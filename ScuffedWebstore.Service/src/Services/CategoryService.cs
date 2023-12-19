using AutoMapper;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Services;
public class CategoryService : BaseService<Category, CategoryReadDTO, CategoryCreateDTO, CategoryUpdateDTO>, ICategoryService
{
    public CategoryService(ICategoryRepo repo, IMapper mapper) : base(repo, mapper)
    {
    }
}