using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Models.Account
{
    public class LoginUserDTO
    {
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
