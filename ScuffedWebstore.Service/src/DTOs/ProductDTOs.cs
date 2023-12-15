using ScuffedWebstore.Core.src.Entities;

namespace ScuffedWebstore.Service.src.DTOs;
public class ProductReadDTO
{
    public Guid ID { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Inventory { get; set; }
    public Guid CategoryID { get; set; }
    public IEnumerable<Image> Images { get; set; }

    public ProductReadDTO Convert(Product product)
    {
        return new ProductReadDTO
        {
            ID = product.ID,
            Title = product.Title,
            Description = product.Description,
            Price = product.Price,
            Inventory = product.Inventory,
            CategoryID = product.CategoryID,
            Images = product.Images
        };
    }
}

public class ProductCreateDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Inventory { get; set; }
    public Guid CategoryID { get; set; }
    public IEnumerable<Image> Images { get; set; }

    public Product Transform()
    {
        return new Product
        {
            ID = new Guid(),
            Title = this.Title,
            Description = this.Description,
            Price = this.Price,
            Inventory = this.Inventory,
            CategoryID = this.CategoryID,
            Images = this.Images
        };
    }
}

public class ProductUpdateDTO
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public double? Price { get; set; }
    public int? Inventory { get; set; }
    public Guid? CategoryID { get; set; }
    public IEnumerable<Image>? Images { get; set; }

    public Product ApplyTo(Product product)
    {
        if (!string.IsNullOrEmpty(this.Title)) product.Title = this.Title;
        if (!string.IsNullOrEmpty(this.Description)) product.Description = this.Description;
        if (this.Price != null) product.Price = (double)this.Price;
        if (this.Inventory != null) product.Inventory = (int)this.Inventory;
        if (this.CategoryID != null) product.CategoryID = (Guid)this.CategoryID;
        if (this.Images != null) product.Images = this.Images;
        return product;
    }
}