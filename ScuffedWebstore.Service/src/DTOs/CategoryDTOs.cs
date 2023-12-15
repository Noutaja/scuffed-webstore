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

    public Category Transform()
    {
        return new Category
        {
            ID = new Guid(),
            Name = this.Name,
            Url = this.Url
        };
    }
}

public class CategoryUpdateDTO
{
    public string? Name { get; set; }
    public string? Url { get; set; }

    public Category ApplyTo(Category category)
    {
        if (!string.IsNullOrEmpty(this.Name)) category.Name = this.Name;
        if (!string.IsNullOrEmpty(this.Url)) category.Url = this.Url;
        return category;
    }
}