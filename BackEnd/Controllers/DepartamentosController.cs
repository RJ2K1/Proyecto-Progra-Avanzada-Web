using BackEnd.Models;
using BackEnd.Services.Implementations;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartamentosController : ControllerBase
    {
        private readonly IDepartamentosService _departamentosService;

        public DepartamentosController(IDepartamentosService departamentosService)
        {
            _departamentosService = departamentosService;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<DepartamentosModel>>> Get()
        {
            var result = await _departamentosService.GetDepartamentos();
            return Ok(result);
        }

        [HttpGet("{id}")]

        public async Task<ActionResult<DepartamentosModel>> Get(int id)
        {
            var result = await _departamentosService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]

        public async Task<ActionResult> Post([FromBody] DepartamentosModel departamento)
        {
            var result = await _departamentosService.Add(departamento);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]

        public async Task<IActionResult> Update(int id, [FromBody] DepartamentosModel departamentoModel)
        {
            if (id != departamentoModel.Id) return BadRequest("ID mismatch");

            var result = await _departamentosService.Update(departamentoModel);
            if (!result) return NotFound("El departamento no existe o no se pudo actualizar.");

            return NoContent(); // Retorna un 204 No Content si la actualización fue exitosa
        }



        [HttpDelete("{id}")]

        public async Task<ActionResult> Delete(int id)
        {
            var result = await _departamentosService.Delete(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
