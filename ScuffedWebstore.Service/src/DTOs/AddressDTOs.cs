namespace ScuffedWebstore.Service.src.DTOs;
public class AddressReadDTO
{
    public Guid ID { get; set; }
    public Guid UserID { get; set; }
    public string Street { get; set; }
    public string Zipcode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}

public class AddressCreateDTO
{
    public Guid UserID { get; set; }
    public string Street { get; set; }
    public string Zipcode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
}

public class AddressUpdateDTO
{
    public string? Street { get; set; }
    public string? Zipcode { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }
}
