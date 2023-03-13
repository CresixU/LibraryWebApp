using LibraryAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/roles")]
    public class RoleController : ControllerBase
    {
        private readonly LibraryContext _dbContext;

        public RoleController(LibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Role>> GetAll()
        {
            var roles = _dbContext.Roles.ToList();

            return Ok(roles);
        }
    }
}
