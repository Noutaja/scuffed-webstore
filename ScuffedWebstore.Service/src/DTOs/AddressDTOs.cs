using ScuffedWebstore.Core.src.Entities;

namespace ScuffedWebstore.Service.src.DTOs;
public class AddressReadDTO
{
    public Guid ID { get; set; }
    public string Street { get; set; }
    public string Zipcode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    public AddressReadDTO Convert(Address address)
    {
        return new AddressReadDTO
        {
            ID = address.ID,
            Street = address.Street,
            Zipcode = address.Zipcode,
            City = address.City,
            Country = address.Country
        };
    }
}

public class AddressCreateDTO
{
    public string Street { get; set; }
    public string Zipcode { get; set; }
    public string City { get; set; }
    public string Country { get; set; }

    public Address Transform()
    {
        return new Address
        {
            ID = new Guid(),
            Street = this.Street,
            Zipcode = this.Zipcode,
            City = this.City,
            Country = this.Country
        };
    }
}

public class AddressUpdateDTO
{
    public string? Street { get; set; }
    public string? Zipcode { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }

    public Address ApplyTo(Address address)
    {
        if (!string.IsNullOrEmpty(this.Street)) address.Street = this.Street;
        if (!string.IsNullOrEmpty(this.Zipcode)) address.Zipcode = this.Zipcode;
        if (!string.IsNullOrEmpty(this.City)) address.City = this.City;
        if (!string.IsNullOrEmpty(this.Country)) address.Country = this.Country;
        return address;
    }
}
