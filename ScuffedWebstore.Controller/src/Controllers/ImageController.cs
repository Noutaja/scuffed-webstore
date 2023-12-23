
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Services;

namespace ScuffedWebstore.Controller.src.Controllers;
public class ImageController : BaseController<Image, IImageService, ImageReadDTO, ImageCreateDTO, ImageUpdateDTO>
{
    public ImageController(IImageService service) : base(service)
    {
    }

    [Authorize(Roles = "Admin")]
    public override async Task<ActionResult<ImageReadDTO>> CreateOne([FromBody] ImageCreateDTO createObject)
    {
        return await base.CreateOne(createObject);
    }

    [Authorize(Roles = "Admin")]
    public override async Task<ActionResult<ImageReadDTO>> UpdateOne([FromRoute] Guid id, [FromBody] ImageUpdateDTO updateObject)
    {
        return await base.UpdateOne(id, updateObject);
    }

    [Authorize(Roles = "Admin")]
    public override async Task<ActionResult<bool>> DeleteOne([FromRoute] Guid id)
    {
        return await base.DeleteOne(id);
    }

    [Authorize(Roles = "Admin")]
    public override async Task<ActionResult<IEnumerable<ImageReadDTO>>> GetAll([FromQuery] GetAllParams getAllParams)
    {
        return await base.GetAll(getAllParams);
    }

    [Authorize(Roles = "Admin")]
    public override async Task<ActionResult<ImageReadDTO?>> GetOneById([FromRoute] Guid id)
    {
        return await base.GetOneById(id);
    }
}