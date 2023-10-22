using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Domain.Entities
{
    [Owned]
    public class Address
    {
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
    }
}
