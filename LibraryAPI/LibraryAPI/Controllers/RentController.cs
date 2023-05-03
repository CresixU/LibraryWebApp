using LibraryAPI.Entities;
using LibraryAPI.Models;
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
        public ActionResult<IEnumerable<RentDTO>> GetAll([FromQuery]LibraryQuery query)
        {
            var rents = _service.GetAll(query).Result;

            return Ok(rents);
        }

        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<RentDTO>> GetAllByUserId([FromRoute] int userId, [FromQuery]LibraryQuery query)
        {
            var rents = _service.GetAllByUserId(userId, query).Result;

            return Ok(rents);
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
