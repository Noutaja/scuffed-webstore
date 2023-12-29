using ScuffedWebstore.Core.src.Entities;

namespace ScuffedWebstore.Service.src.DTOs;
public class ProductReadDTO : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Inventory { get; set; }
    public CategoryReadDTO Category { get; set; }
    public IEnumerable<ImageReadDTO> Images { get; set; }
}

public class ProductWithoutPriceReadDTO : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int Inventory { get; set; }
    public CategoryReadDTO Category { get; set; }
    public IEnumerable<ImageReadDTO> Images { get; set; }
}

public class ProductCreateDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Inventory { get; set; }
    public Guid CategoryID { get; set; }
    public IEnumerable<ImageCreateDTO> Images { get; set; }
}

public class ProductUpdateDTO
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public double? Price { get; set; }
    public int? Inventory { get; set; }
    public Guid? CategoryID { get; set; }
    public IEnumerable<ImageUpdateDTO>? UpdatedImages { get; set; }
    public IEnumerable<ImageCreateDTO>? NewImages { get; set; }
    public IEnumerable<Guid>? DeletedImages { get; set; }
}