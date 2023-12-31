namespace ScuffedWebstore.Core.src.Entities
{
    public class Address : OwnedEntity
    {
        public string Street { get; set; }
        public string Zipcode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public bool Hidden { get; set; }
    }
}