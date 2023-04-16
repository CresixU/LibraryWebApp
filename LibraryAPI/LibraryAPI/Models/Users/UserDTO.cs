using LibraryAPI.Models.Rents;

namespace LibraryAPI.Models.Users
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }

        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string PostalCode { get; set; }
        public string Role { get; set; }
        public List<RentDTO> Rents { get; set; }
    }
}
