using ScuffedWebstore.Core.src.Abstractions;
using ScuffedWebstore.Core.src.Types;

namespace ScuffedWebstore.Core.src.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public byte[] Password { get; set; }
    public byte[] Salt { get; set; }
    public string Avatar { get; set; }
    public UserRole Role { get; set; }
    public IEnumerable<Address> Addresses { get; set; }
    public IEnumerable<Order> Orders { get; set; }
    public IEnumerable<Review> Reviews { get; set; }
    public IEnumerable<CartItem> CartItems { get; set; }
}
