using ScuffedWebstore.Core.src.Entities;

namespace ScuffedWebstore.Service.src.DTOs;
public class AddressReadDTO : BaseEntity
{
    public UserReadDTO User { get; set; }
    public string Street { get; set; }
    public string Zipcode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public bool Hidden { get; set; }
}

public class AddressCreateDTO
{
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
    public bool Hidden { get; set; }
}
