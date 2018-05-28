namespace Indexing.Domain.Entities
{
    public class Address
    {
        public Address()
        {
        }

        public Address(string street, string zipCode)
        {
            Street = street;
            ZipCode = zipCode;
        }

        public string Street { get; set; }

        public string ZipCode { get; set; }
    }
}
