using BackEnd.Models;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {

        private readonly ITicketsService _serviceTicket;
        public TicketController(ITicketsService serviceTicket)
        {
            _serviceTicket = serviceTicket;
        }


        // GET: api/<TicketController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketModel>>> Get()
        {
            var result = await _serviceTicket.GetTickts();
            return Ok(result);
        }

        // GET api/<TicketController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketModel>> Get(int id)
        {
            var result = await _serviceTicket.getById(id);
            if (result == null)
            {

                return NotFound();
            }
            return Ok(result);


        }

        // POST api/<TicketController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TicketModel ticket)
        {
            var result = await _serviceTicket.add(ticket);
            if (result) {
                return Ok();
            }
            else { return BadRequest(); 
            }

        }

        // PUT api/<TicketController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] TicketModel ticket)
        {
            var result = await _serviceTicket.Update(ticket);
            if (result)
            {
                return Ok();
            }
            else {
                return BadRequest();
            }
        }

        // DELETE api/<TicketController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _serviceTicket.delete(id);
            if (result)
            {
                return Ok();
            }
            else {
                return BadRequest();
            }
        }
    }
}
