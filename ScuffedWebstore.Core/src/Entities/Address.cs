using ScuffedWebstore.Core.src.Abstractions;

namespace ScuffedWebstore.Core.src.Entities
{
    public class Address : BaseEntity
    {
        public Guid UserID { get; set; }
        public string Street { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}