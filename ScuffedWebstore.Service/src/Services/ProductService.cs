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

    public override ProductReadDTO CreateOne(ProductCreateDTO createObject)
    {
        Category? c = _categoryRepo.GetOneById(createObject.CategoryID);
        if (c == null) throw CustomException.NotFoundException("Category not found");

        Product product = _mapper.Map<ProductCreateDTO, Product>(createObject);
        /* product.Category = c; */

        return _mapper.Map<Product, ProductReadDTO>(_repo.CreateOne(product));
    }
}