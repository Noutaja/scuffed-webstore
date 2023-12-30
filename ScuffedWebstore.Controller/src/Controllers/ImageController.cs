/* using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScuffedWebstore.Core.src.Entities;
using ScuffedWebstore.Core.src.Parameters;
using ScuffedWebstore.Service.src.Abstractions;
using ScuffedWebstore.Service.src.DTOs;
using ScuffedWebstore.Service.src.Services;

namespace ScuffedWebstore.Controller.src.Controllers;
[Authorize(Roles = "Admin")]
public class ImageController : BaseController<Image, IImageService, ImageReadDTO, ImageCreateDTO, ImageUpdateDTO>
{

    public ImageController(IImageService service) : base(service)
    {
    }
} */