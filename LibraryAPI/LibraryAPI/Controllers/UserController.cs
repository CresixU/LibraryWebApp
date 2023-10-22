using LibraryAPI.Application.Models.Users;
using LibraryAPI.Application.Services;
using LibraryAPI.Domain.Services.Page;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsersDTO>>> GetAll([FromQuery]LibraryQuery query)
        {
            var users = await _userService.GetAll(query);

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> Get([FromRoute] int id)
        {
            var user = await _userService.GetById(id);

            if (user is null) return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] UserCreateDTO dto)
        {
            var id = await _userService.Create(dto);

            return Created($"api/users/{id}", null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser([FromRoute] int id, [FromBody] UserUpdateDTO dto)
        {
            var isUpdated = await _userService.Update(id, dto);

            if (isUpdated) return Ok();

            return NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser([FromRoute] int id)
        {
            var isDeleted = await _userService.Delete(id);

            if (isDeleted) return NoContent();

            return NotFound();
        }

        [HttpPut("{id}/role/{roleId}")]
        public async Task<ActionResult> UserUpdateRole([FromRoute] int id, [FromRoute] int roleId)
        {
            var isUpdated = await _userService.RoleUpdate(id, roleId);

            if (isUpdated) return Ok();

            return NotFound();
        }
    }
}
