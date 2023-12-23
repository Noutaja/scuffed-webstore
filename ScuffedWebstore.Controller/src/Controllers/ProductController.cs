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
    public override async Task<ActionResult<IEnumerable<ProductReadDTO>>> GetAll([FromQuery] GetAllParams getAllParams)
    {
        return await base.GetAll(getAllParams);
    }

    [AllowAnonymous]
    public override async Task<ActionResult<ProductReadDTO?>> GetOneById([FromRoute] Guid id)
    {
        return await base.GetOneById(id);
    }

    [Authorize(Roles = "Admin")]
    public override Task<ActionResult<ProductReadDTO>> CreateOne([FromBody] ProductCreateDTO createObject)
    {
        return base.CreateOne(createObject);
    }

    [Authorize(Roles = "Admin")]
    public override Task<ActionResult<ProductReadDTO>> UpdateOne([FromRoute] Guid id, [FromBody] ProductUpdateDTO updateObject)
    {
        return base.UpdateOne(id, updateObject);
    }

    [Authorize(Roles = "Admin")]
    public override Task<ActionResult<bool>> DeleteOne([FromRoute] Guid id)
    {
        return base.DeleteOne(id);
    }
}