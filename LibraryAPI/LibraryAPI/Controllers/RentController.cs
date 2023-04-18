using LibraryAPI.Entities;
using LibraryAPI.Models.Rents;
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
            var rents = _service.GetAll().Result;

            return Ok(rents);
        }

        [HttpGet("{id}")]
        public ActionResult<Rent> GetAllByUserId([FromRoute] int id)
        {
            var rent = _service.GetAllByUserId(id).Result;

            if (rent is null) return NotFound();

            return Ok(rent);
        }

        [HttpPost]
        public ActionResult RentBooks([FromBody]RentCreateDTO dto)
        {
            var id = _service.RentBooks(dto).Result;
            if (id == 0) return NoContent();
            return Created($"api/rents/{id}", null);
        }

        [HttpPut("{rentId}")]
        public ActionResult ReturnBooks([FromRoute]int rentId, [FromBody]RentReturnDTO dto)
        {
            var isUpdated = _service.ReturnBooks(rentId, dto).Result;
            if(!isUpdated) return NotFound();

            return Ok();
        }
    }
}
