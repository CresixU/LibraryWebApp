using System.ComponentModel.DataAnnotations;

namespace LibraryAPI.Application.Models.Account
{
    public class LoginUserDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
