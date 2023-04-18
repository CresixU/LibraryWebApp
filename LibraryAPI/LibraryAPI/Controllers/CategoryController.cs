using LibraryAPI.Models.Categories;
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
            var categories = _service.GetAll().Result;

            return Ok(categories);
        }

        [HttpPost]
        public ActionResult Create([FromBody] CategoryDTO dto)
        {
            var id = _service.Create(dto).Result;

            return Created($"api/categories/{id}", null);
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromRoute] int id, [FromBody] CategoryDTO dto)
        {
            var isUpdated = _service.Update(id, dto).Result;

            if (!isUpdated) return NotFound();

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
