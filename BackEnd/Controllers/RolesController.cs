using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;
using BackEnd.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesService _rolesService;

        public RolesController(IRolesService rolesService)
        {
            _rolesService = rolesService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RolesModel>>> GetAll()
        {
            var roles = await _rolesService.GetAllRoles();
            return Ok(roles);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RolesModel>> GetById(int id)
        {
            var rol = await _rolesService.GetById(id);
            if (rol == null) return NotFound();
            return Ok(rol);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] RolesModel rolModel)
        {
            var result = await _rolesService.Add(rolModel);
            if (!result) return BadRequest();
            return CreatedAtAction(nameof(GetById), new { id = rolModel.Id }, rolModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] RolesModel rolModel)
        {
            if (id != rolModel.Id) return BadRequest("ID mismatch");
            var result = await _rolesService.Update(rolModel);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _rolesService.Delete(id);
            if (!result) return NotFound();
            return NoContent();
        }
    }
}
