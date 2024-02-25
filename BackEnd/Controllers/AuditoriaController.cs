// AuditoriaController.cs
using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;
using BackEnd.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuditoriaController : ControllerBase
    {
        private readonly IAuditoriaService _auditoriaService;

        public AuditoriaController(IAuditoriaService auditoriaService)
        {
            _auditoriaService = auditoriaService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditoriaModel>>> GetAll()
        {
            var auditorias = await _auditoriaService.GetAuditorias();
            return Ok(auditorias);
        }

        // Agrega más métodos según sea necesario.
    }
}
