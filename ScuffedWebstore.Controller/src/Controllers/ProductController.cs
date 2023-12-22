using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Services;

namespace ScuffedWebstore.Controller.src.Controllers;
public class ProductController : BaseController<Product, IProductService, ProductReadDTO, ProductCreateDTO, ProductUpdateDTO>
{
    public ProductController(IProductService service) : base(service)
    {
    }

    [AllowAnonymous]
    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<ProductReadDTO>>> GetAllAsync([FromQuery] GetAllProductsParams getAllParams)
    {
        return await base.GetAllAsync(getAllParams);
    }

    [AllowAnonymous]
    public override async Task<ActionResult<IEnumerable<ProductReadDTO>>> GetAllAsync([FromQuery] GetAllParams getAllParams)
    {
        return await base.GetAllAsync(getAllParams);
    }

    [AllowAnonymous]
    public override async Task<ActionResult<ProductReadDTO?>> GetOneByIdAsync([FromRoute] Guid id)
    {
        return await base.GetOneByIdAsync(id);
    }
}