using LibraryAPI.Entities;
using LibraryAPI.Models;
using LibraryAPI.Models.Books;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAll([FromQuery] LibraryQuery query)
        {
            var books = await _service.GetAll(query);
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<BookDTO>>> Get([FromRoute] int id)
        {
            var book = await _service.Get(id);

            if(book is null)
                return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] BookCreateDTO dto)
        {
            var bookId = await _service.Create(dto);

            return Created($"api/books/{bookId}", null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] BookUpdateDTO dto)
        {
            var isUpdated = await _service.Update(id, dto);
            
            if(!isUpdated)
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var isDeleted = await _service.Delete(id);

            if(!isDeleted)
                return NotFound();

            return NoContent();
        }
    }
}
