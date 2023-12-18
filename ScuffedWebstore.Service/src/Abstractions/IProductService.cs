using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Service.src.Abstractions;
public interface IProductService : IBaseService<Product, ProductReadDTO, ProductCreateDTO, ProductUpdateDTO>
{
    public ProductReadDTO CreateOne(ProductCreateDTO address);
    public bool DeleteOne(Guid id);
    public IEnumerable<ProductReadDTO> GetAll(GetAllParams options);
    public ProductReadDTO? GetOneById(Guid id);
    public ProductReadDTO UpdateOne(Guid id, ProductUpdateDTO address);
}