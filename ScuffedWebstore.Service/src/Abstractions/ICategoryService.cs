using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Abstractions;
public interface ICategoryService
{
    public CategoryReadDTO CreateOne(CategoryCreateDTO address);
    public bool DeleteOne(Guid id);
    public IEnumerable<CategoryReadDTO> GetAll(GetAllParams options);
    public CategoryReadDTO? GetOneById(Guid id);
    public CategoryReadDTO UpdateOne(Guid id, CategoryUpdateDTO address);
}