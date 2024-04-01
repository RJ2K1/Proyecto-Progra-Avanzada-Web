using BackEnd.Models;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosService _usuariosService;
        private readonly IAuthenticationService _authService;

        public UsuariosController(IUsuariosService usuariosService, IAuthenticationService authService)
        {
            _usuariosService = usuariosService;
            _authService = authService;
        }


        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuariosModel>>> Get()
        {
            var result = await _usuariosService.GetUsuarios();
            return Ok(result);
        }

        // GET api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuariosModel>> Get(int id)
        {
            var result = await _usuariosService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST api/Usuarios
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UsuariosModel usuario)
        {
            var result = await _usuariosService.Add(usuario);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT api/Usuarios
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UsuariosModel usuario)
        {
            var result = await _usuariosService.Update(usuario);
            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _usuariosService.Delete(id);
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

