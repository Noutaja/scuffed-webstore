
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Controller.src.Controllers;
public class ImageController : BaseController<Image, ImageReadDTO, ImageCreateDTO, ImageUpdateDTO>
{
    public ImageController(IImageService service) : base(service)
    {
    }

    [Authorize]
    public override ActionResult<ImageReadDTO> CreateOne([FromBody] ImageCreateDTO createObject)
    {
        return base.CreateOne(createObject);
    }

    [Authorize]
    public override ActionResult<ImageReadDTO> UpdateOne([FromRoute] Guid id, [FromBody] ImageUpdateDTO updateObject)
    {
        return base.UpdateOne(id, updateObject);
    }
}