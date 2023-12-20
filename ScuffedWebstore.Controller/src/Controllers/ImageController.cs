
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Services;

namespace ScuffedWebstore.Controller.src.Controllers;
public class ImageController : BaseController<Image, ImageService, ImageReadDTO, ImageCreateDTO, ImageUpdateDTO>
{
    public ImageController(ImageService service) : base(service)
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