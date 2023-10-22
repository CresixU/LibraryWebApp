using LibraryAPI.Application.Models.Categories;
using LibraryAPI.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/categories")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll()
        {
            var categories = await _service.GetAll();

            return Ok(categories);
        }


        [HttpPost]
        public async Task<ActionResult> Create([FromBody] CategoryDTO dto)
        {
            var id = await _service.Create(dto);

            return Created($"api/categories/{id}", null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute] int id, [FromBody] CategoryDTO dto)
        {
            var isUpdated = await _service.Update(id, dto);

            if (!isUpdated) return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete([FromRoute] int id)
        {
            var isDeleted = await _service.Delete(id);

            if(!isDeleted) return NotFound();

            return NoContent();
        }
    }
}
