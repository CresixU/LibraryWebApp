using LibraryAPI.Entities;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/rents")]
    public class RentController : ControllerBase
    {
        private readonly IRentService _service;

        public RentController(IRentService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Rent>> GetAll()
        {
            var rents = _service.GetAll();

            return Ok(rents);
        }

        [HttpGet("{id}")]
        public ActionResult<Rent> GetAllByUserId([FromRoute] int id)
        {
            var rent = _service.GetAllByUserId(id);

            if (rent is null) return NotFound();

            return Ok(rent);
        }

        [HttpPost]
        public ActionResult RentBooks(List<int> bookIds)
        {
            var id = _service.RentBooks(bookIds);
            if (id == 0) return NoContent();
            return Created($"api/rents/{id}", null);
        }

        [HttpPut("{id}")]
        public ActionResult ReturnBooks(int rentId, List<int> bookIds)
        {
            var isUpdated = _service.ReturnBooks(rentId, bookIds);
            if(!isUpdated) return NotFound();

            return Ok();
        }
    }
}
