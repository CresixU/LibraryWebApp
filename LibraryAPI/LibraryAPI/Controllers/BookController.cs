using LibraryAPI.Entities;
using LibraryAPI.Models;
using LibraryAPI.Models.Books;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookDTO>> GetAll([FromQuery]LibraryQuery query)
        {
            var books = _service.GetAll(query).Result;
            return Ok(books);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<BookDTO>> Get([FromRoute] int id)
        {
            var book = _service.Get(id).Result;

            if(book is null) return NotFound();

            return Ok(book);
        }

        [HttpPost]
        public ActionResult Create([FromBody] BookCreateDTO dto)
        {
            var bookId = _service.Create(dto).Result;

            return Created($"api/books/{bookId}", null);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] BookUpdateDTO dto)
        {
            var isUpdated = _service.Update(id, dto).Result;
            
            if(!isUpdated) return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _service.Delete(id).Result;

            if(!isDeleted) return NotFound();

            return NoContent();
        }
    }
}
