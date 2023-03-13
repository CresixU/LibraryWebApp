using LibraryAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Controllers
{
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly LibraryContext _dbContext;

        public UserController(LibraryContext libraryContext)
        {
            _dbContext = libraryContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            var users = _dbContext
                .Users
                .ToList();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> Get([FromRoute] int id)
        {
            var user = _dbContext
                .Users
                .Include(u => u.Address)
                .Include(u => u.Rents)
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Id == id);

            if (user is null) return NotFound();

            return Ok(user);
        }
    }
}
