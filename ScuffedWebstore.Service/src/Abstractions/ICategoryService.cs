using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Abstractions;
public interface ICategoryService : IBaseService<Category, CategoryReadDTO, CategoryCreateDTO, CategoryUpdateDTO>
{

}