using ScuffedWebstore.Core.src.Entities;

namespace ScuffedWebstore.Service.src.DTOs;
public class CategoryReadDTO
{
    public Guid ID { get; set; }
    public string Name { get; set; }
    public string Url { get; set; }

    public CategoryReadDTO Convert(Category category)
    {
        return new CategoryReadDTO
        {
            ID = category.ID,
            Name = category.Name,
            Url = category.Url
        };
    }
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