using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ScuffedWebstore.Core.src.Entities;

namespace ScuffedWebstore.Service.src.DTOs;
public class ImageReadDTO
{
    public Guid ID { get; set; }
    public Guid ProductID { get; set; }
    public string Url { get; set; }

    public ImageReadDTO Convert(Image image)
    {
        return new ImageReadDTO
        {
            ID = image.ID,
            ProductID = image.ProductID,
            Url = image.Url
        };
    }
}

public class ImageCreateDTO
{
    public Guid ProductID { get; set; }
    public string Url { get; set; }

    public Image Transform()
    {
        return new Image
        {
            ID = new Guid(),
            ProductID = this.ProductID,
            Url = this.Url
        };
    }
}

public class ImageUpdateDTO
{
    public string? Url { get; set; }

    public Image ApplyTo(Image image)
    {
        if (!string.IsNullOrEmpty(this.Url)) image.Url = this.Url;
        return image;
    }
}