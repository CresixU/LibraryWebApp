using LibraryAPI.Models.Account;
using LibraryAPI.Models.Users;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
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
            return Created($"api/account/{id}",null);
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginUserDTO dto)
        {
            //todo walidacja LoginUserDTO
            string token = _service.GenerateJwt(dto);
            return Ok(token);
        }


        
    }
}
