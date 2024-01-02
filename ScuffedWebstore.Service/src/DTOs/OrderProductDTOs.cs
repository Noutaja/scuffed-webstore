using ScuffedWebstore.Core.src.Entities;

namespace ScuffedWebstore.Service.src.DTOs;
public class OrderProductReadDTO : Timestamp
{
    public ProductWithoutPriceReadDTO Product { get; set; }
    public int Amount { get; set; }
    public double Price { get; set; }
}

public class OrderProductCreateDTO
{
    public Guid ProductID { get; set; }
    public int Amount { get; set; }
}