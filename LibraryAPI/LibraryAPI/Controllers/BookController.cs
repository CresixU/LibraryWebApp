using LibraryAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/books")]
    public class BookController : ControllerBase
    {
        private readonly LibraryContext _dbContext;

        public BookController(LibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetAll()
        {
            var books = _dbContext.Books.ToList();

            return Ok(books);
        }

        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Book>> Get([FromRoute] int id)
        {
            var book = _dbContext
                .Books
                .FirstOrDefault(x => x.Id == id);

            if (book is null) return NotFound();

            return Ok(book);
        }
    }
}
