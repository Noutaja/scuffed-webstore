using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Services;
public class CategoryService : ICategoryService
{
    public CategoryReadDTO CreateOne(CategoryCreateDTO address)
    {
        throw new NotImplementedException();
    }

    public bool DeleteOne(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<CategoryReadDTO> GetAll(GetAllParams options)
    {
        throw new NotImplementedException();
    }

    public CategoryReadDTO? GetOneById(Guid id)
    {
        throw new NotImplementedException();
    }

    public CategoryReadDTO UpdateOne(Guid id, CategoryUpdateDTO address)
    {
        throw new NotImplementedException();
    }
}