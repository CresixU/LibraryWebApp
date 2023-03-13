using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public int Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateOnly Birthdate { get; set; }
        
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public List<Rent> Rents { get; set; }

    }
}
