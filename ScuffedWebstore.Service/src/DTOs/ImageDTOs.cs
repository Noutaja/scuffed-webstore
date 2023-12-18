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
}

public class ImageCreateDTO
{
    public Guid ProductID { get; set; }
    public string Url { get; set; }
}

public class ImageUpdateDTO
{
    public string? Url { get; set; }
}