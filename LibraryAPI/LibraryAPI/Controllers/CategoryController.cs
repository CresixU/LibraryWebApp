using LibraryAPI.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly LibraryContext _dbContext;

        public CategoryController(LibraryContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> GetAll()
        {
            var categories = _dbContext.Categories.ToList();

            return Ok(categories);
        }
    }
}
