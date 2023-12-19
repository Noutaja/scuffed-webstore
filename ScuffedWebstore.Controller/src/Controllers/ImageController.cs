
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;

namespace ScuffedWebstore.Controller.src.Controllers;
public class ImageController : BaseController<Image, ImageReadDTO, ImageCreateDTO, ImageUpdateDTO>
{
    public ImageController(IImageService service) : base(service)
    {
    }
}