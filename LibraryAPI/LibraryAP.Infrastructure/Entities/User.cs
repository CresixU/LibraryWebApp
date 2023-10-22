using Microsoft.AspNetCore.Identity;

namespace LibraryAPI.Infrastructure.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool isDeleted { get; set; } = false;
        public Address Address { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
        public List<Rent> Rents { get; set; } = new List<Rent>();

    }
}
