using AutoMapper;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Shared;

namespace ScuffedWebstore.Service.src.Services;
public class CategoryService : BaseService<Category, CategoryReadDTO, CategoryCreateDTO, CategoryUpdateDTO>, ICategoryService
{
    public CategoryService(ICategoryRepo repo, IMapper mapper) : base(repo, mapper)
    {
    }

    public override Task<CategoryReadDTO> CreateOneAsync(Guid id, CategoryCreateDTO createObject)
    {
        if (createObject.Name.Length < 3)
        {
            throw CustomException.InvalidParameters("Name can't be less than 3 characters long");
        }
        if (!Uri.IsWellFormedUriString(createObject.Url, UriKind.Absolute))
        {
            throw CustomException.InvalidParameters("Not a valid URI");
        }

        return base.CreateOneAsync(id, createObject);
    }

    public override Task<CategoryReadDTO> UpdateOneAsync(Guid id, CategoryUpdateDTO updateObject)
    {
        if (updateObject.Name != null && updateObject.Name.Length < 3)
        {
            throw CustomException.InvalidParameters("Name can't be less than 3 characters long");
        }
        if (updateObject.Url != null &&
        !Uri.IsWellFormedUriString(updateObject.Url, UriKind.Absolute))
        {
            throw CustomException.InvalidParameters("Not a valid URI");
        }
        return base.UpdateOneAsync(id, updateObject);
    }
}