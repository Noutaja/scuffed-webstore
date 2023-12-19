using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Controller.src.Controllers;
public class ProductController : BaseController<Product, ProductReadDTO, ProductCreateDTO, ProductUpdateDTO>
{
    public ProductController(IProductService service) : base(service)
    {
    }
}