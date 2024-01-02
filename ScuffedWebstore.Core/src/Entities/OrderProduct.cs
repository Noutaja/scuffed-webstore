namespace ScuffedWebstore.Core.src.Entities;
public class OrderProduct : Timestamp
{
    public Product Product { get; set; }
    public Guid ProductID { get; set; }
    public Guid OrderID { get; set; }
    public int Amount { get; set; }
    public double Price { get; set; }
}