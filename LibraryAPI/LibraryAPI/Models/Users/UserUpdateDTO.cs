using LibraryAPI.Entities;
using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models.Users
{
    public class UserUpdateDTO
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Firstname { get; set; }
        [MaxLength(50)]
        public string Lastname { get; set; }
        [MaxLength(50)]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string PostalCode { get; set; }
    }
}
