using LibraryAPI.Application.Models.Rents;
using LibraryAPI.Application.Services;
using LibraryAPI.Entities;
using LibraryAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/rents")]
    public class RentController : ControllerBase
    {
        private readonly IRentService _service;

        public RentController(IRentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RentDTO>>> GetAll([FromQuery]LibraryQuery query)
        {
            var rents = await _service.GetAll(query);

            return Ok(rents);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<RentDTO>>> GetAllByUserId([FromRoute] int userId, [FromQuery]LibraryQuery query)
        {
            var rents = await _service.GetAllByUserId(userId, query);

            return Ok(rents);
        }

        [HttpPost]
        public async Task<ActionResult> RentBooks([FromBody]RentCreateDTO dto)
        {
            var id = await _service.RentBook(dto);
            if (id == 0) return NoContent();
            return Created($"api/rents/{id}", null);
        }

        [HttpPut("{rentId}")]
        public async Task<ActionResult> ReturnBooks([FromRoute]int rentId, [FromBody]RentReturnDTO dto)
        {
            var isUpdated = await _service.ReturnBook(rentId, dto);
            if(!isUpdated) return NotFound();

            return Ok();
        }

        [HttpDelete("{rentId}")]
        public async Task<ActionResult> DeleteRent([FromRoute]int rentId)
        {
            var isDeleted = await _service.DeleteRent(rentId);
            if (!isDeleted) return NotFound();

            return NoContent();
        }
    }
}
