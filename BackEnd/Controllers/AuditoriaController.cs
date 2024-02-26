// AuditoriaController.cs
using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;
using BackEnd.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditoriaController : ControllerBase
    {
        private readonly IAuditoriaService _auditoriaService;

        public AuditoriaController(IAuditoriaService auditoriaService)
        {
            _auditoriaService = auditoriaService;
        }

        // GET: api/Auditoria
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditoriaModel>>> Get()
        {
            var result = await _auditoriaService.GetAuditorias();
            return Ok(result);
        }

        // GET api/Auditoria/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuditoriaModel>> Get(int id)
        {
            var result = await _auditoriaService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST api/Auditoria
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] AuditoriaModel auditoria)
        {
            var result = await _auditoriaService.Add(auditoria);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/Auditoria
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] AuditoriaModel auditoria)
        {
            var result = await _auditoriaService.Update(auditoria);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/Auditoria/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _auditoriaService.Delete(id);
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
