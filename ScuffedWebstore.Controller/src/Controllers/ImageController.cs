
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Services;

namespace ScuffedWebstore.Controller.src.Controllers;
public class ImageController : BaseController<Image, IImageService, ImageReadDTO, ImageCreateDTO, ImageUpdateDTO>
{
    public ImageController(IImageService service) : base(service)
    {
    }

    //[Authorize]
    public override async Task<ActionResult<ImageReadDTO>> CreateOne([FromBody] ImageCreateDTO createObject)
    {
        return await base.CreateOne(createObject);
    }

    //[Authorize]
    public override async Task<ActionResult<ImageReadDTO>> UpdateOne([FromRoute] Guid id, [FromBody] ImageUpdateDTO updateObject)
    {
        return await base.UpdateOne(id, updateObject);
    }
}