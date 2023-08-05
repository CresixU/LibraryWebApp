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
        public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDTO dto)
        {
            var id = await _service.RegisterUser(dto);
            if (id == -1)
                return Conflict("Email taken"); 

            return Created($"api/account/{id}",null);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDTO dto)
        {
            string token = await _service.GenerateJwt(dto);
            return Ok(token);
        }

        [HttpGet("checkmail")]
        public async Task<ActionResult> IsEmailAvailable([FromQuery] string email)
        {

            if(await _service.IsEmailAvailable(email))
                return Ok(true);

            return Conflict("Email taken");
        }


        
    }
}
