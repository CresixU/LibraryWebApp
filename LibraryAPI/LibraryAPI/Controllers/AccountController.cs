using LibraryAPI.Models.Account;
using LibraryAPI.Models.Users;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _service;

        public AccountController(IAccountService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public ActionResult RegisterUser([FromBody] RegisterUserDTO dto)
        {
            var id = _service.RegisterUser(dto).Result;
            if (id == -1) return Conflict("Email taken"); 
            return Created($"api/account/{id}",null);
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginUserDTO dto)
        {
            string token = _service.GenerateJwt(dto).Result;
            return Ok(token);
        }

        [HttpGet("checkmail")]
        public ActionResult IsEmailAvailable([FromQuery] string email)
        {

            if(_service.IsEmailAvailable(email).Result) return Ok(true);
            else return Conflict("Email taken");
        }


        
    }
}
