using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Services;

namespace ScuffedWebstore.Controller.src.Controllers;
public class ProductController : BaseController<Product, ProductService, ProductReadDTO, ProductCreateDTO, ProductUpdateDTO>
{
    public ProductController(ProductService service) : base(service)
    {
    }

    [AllowAnonymous]
    public override ActionResult<IEnumerable<ProductReadDTO>> GetAll([FromQuery] GetAllParams getAllParams)
    {
        return base.GetAll(getAllParams);
    }

    [AllowAnonymous]
    public override ActionResult<ProductReadDTO>? GetOneById([FromRoute] Guid id)
    {
        return base.GetOneById(id);
    }
}