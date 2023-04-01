using LibraryAPI.Models.Category;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> GetAll()
        {
            var categories = _service.GetAll();

            return Ok(categories);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CategoryDTO dto)
        {
            var id = _service.Create(dto);

            return Created($"api/categories/{id}", null);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] CategoryDTO dto)
        {
            var isUpdated = _service.Update(id, dto);

            if (!isUpdated) return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var isDeleted = _service.Delete(id);

            if(!isDeleted) return NotFound();

            return NoContent();
        }
    }
}
