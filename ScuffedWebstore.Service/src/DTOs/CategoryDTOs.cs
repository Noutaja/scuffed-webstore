using ScuffedWebstore.Core.src.Entities;

namespace ScuffedWebstore.Service.src.DTOs;
public class CategoryReadDTO
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }
}

public class CategoryCreateDTO
{
    public string Name { get; set; }
    public string Url { get; set; }
}

public class CategoryUpdateDTO
{
    public string? Name { get; set; }
    public string? Url { get; set; }
}