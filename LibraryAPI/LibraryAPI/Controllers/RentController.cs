using LibraryAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/rents")]
    public class RentController : ControllerBase
    {
        private readonly LibraryContext _dbContext;

        public RentController(LibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Rent>> GetAll()
        {
            var rents = _dbContext.Rents.ToList();

            return Ok(rents);
        }

        [HttpGet("{id}")]
        public ActionResult<Rent> Get([FromRoute] int id)
        {
            var rent = _dbContext
                .Rents
                .FirstOrDefault(r => r.Id == id);

            if (rent is null) return NotFound();

            return Ok(rent);
        }
    }
}
