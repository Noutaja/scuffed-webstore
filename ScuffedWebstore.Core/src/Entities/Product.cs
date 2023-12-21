using ScuffedWebstore.Core.src.Abstractions;

namespace ScuffedWebstore.Core.src.Entities;
public class Product : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Inventory { get; set; }
    public Category Category { get; set; }
    public IEnumerable<Image> Images { get; set; }
    public IEnumerable<Review> Reviews { get; set; }
    public IEnumerable<OrderProduct> OrderProducts { get; set; }
}