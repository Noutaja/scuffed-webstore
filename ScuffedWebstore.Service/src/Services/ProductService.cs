using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Services;
public class ProductService : IProductService
{
    public ProductReadDTO CreateOne(ProductCreateDTO address)
    {
        throw new NotImplementedException();
    }

    public bool DeleteOne(Guid id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ProductReadDTO> GetAll(GetAllParams options)
    {
        throw new NotImplementedException();
    }

    public ProductReadDTO? GetOneById(Guid id)
    {
        throw new NotImplementedException();
    }

    public ProductReadDTO UpdateOne(Guid id, ProductUpdateDTO address)
    {
        throw new NotImplementedException();
    }
}