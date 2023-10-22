using LibraryAPI.Application.Models.Roles;
using LibraryAPI.Entities;
using LibraryAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/roles")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetAll()
        {
            var roles = await _service.GetAll();

            return Ok(roles);
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody] RoleDTO dto)
        {
            var id = await _service.Create(dto);

            return Created($"api/roles/{id}", null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([FromRoute]int id, [FromBody] RoleDTO dto)
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
