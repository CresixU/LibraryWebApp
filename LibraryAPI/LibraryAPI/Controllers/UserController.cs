using AutoMapper;
using LibraryAPI.Entities;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryAPI.Controllers
{
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly LibraryContext _dbContext;
        private readonly IMapper _mapper;

        public UserController(LibraryContext libraryContext, IMapper mapper)
        {
            _dbContext = libraryContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            var users = _dbContext
                .Users
                .Include(u => u.Address)
                .ToList();

            var usersDtos = _mapper.Map<List<UsersDTO>>(users);

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
