using AutoMapper;
using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Shared;

namespace ScuffedWebstore.Service.src.Services;
public class ProductService : BaseService<Product, ProductReadDTO, ProductCreateDTO, ProductUpdateDTO>, IProductService
{
    private ICategoryRepo _categoryRepo;
    public ProductService(IProductRepo repo, ICategoryRepo categoryRepo, IMapper mapper) : base(repo, mapper)
    {
        _categoryRepo = categoryRepo;
    }

    public override async Task<ProductReadDTO> CreateOneAsync(Guid id, ProductCreateDTO createObject)
    {
        Category? c = await _categoryRepo.GetOneByIdAsync(createObject.CategoryID);
        if (c == null) throw CustomException.NotFoundException("Category not found");

        Product product = _mapper.Map<ProductCreateDTO, Product>(createObject);
        product.ID = id;

        return _mapper.Map<Product, ProductReadDTO>(await _repo.CreateOneAsync(product));
    }

    public override async Task<ProductReadDTO> UpdateOneAsync(Guid id, ProductUpdateDTO updateObject)
    {
        Console.WriteLine("UPDATEOBJECT!!!!");
        Console.WriteLine(updateObject);
        foreach (var prop in updateObject.GetType().GetProperties())
        {
            Console.WriteLine(prop.Name);
            Console.WriteLine(prop.GetValue(updateObject));
        }
        Product? currentEntity = await _repo.GetOneByIdAsync(id);
        if (currentEntity == null) throw CustomException.NotFoundException("Not Found");
        Console.WriteLine("CURRENT!!!!");
        Console.WriteLine(currentEntity);
        foreach (var prop in currentEntity.GetType().GetProperties())
        {
            Console.WriteLine(prop.Name);
            Console.WriteLine(prop.GetValue(currentEntity));
        }


        _mapper.Map<ProductUpdateDTO, Product>(updateObject, currentEntity);
        Console.WriteLine("UPDATED!!!!");
        Console.WriteLine(currentEntity);
        foreach (var prop in currentEntity.GetType().GetProperties())
        {
            Console.WriteLine(prop.Name);
            Console.WriteLine(prop.GetValue(currentEntity));
        }

        return _mapper.Map<Product, ProductReadDTO>(await _repo.UpdateOneAsync(currentEntity));
    }
}