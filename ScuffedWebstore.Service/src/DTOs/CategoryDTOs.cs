using ScuffedWebstore.Core.src.Entities;

namespace ScuffedWebstore.Service.src.DTOs;
public class CategoryReadDTO : BaseEntity
{
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