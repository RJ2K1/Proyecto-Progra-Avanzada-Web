using BackEnd.Models;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketsService _ticketsService;
        private readonly ILogger<TicketController> _logger;

        public TicketController(ITicketsService ticketsService, ILogger<TicketController> logger)
        {
            _ticketsService = ticketsService;
            _logger = logger;

        }

        // GET: api/Ticket
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketModel>>> Get()
        {
            var result = await _ticketsService.GetTickets();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // GET: api/Ticket/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketModel>> Get(int id)
        {
            var result = await _ticketsService.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/Ticket
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TicketModel ticketModel)
        {
            var result = await _ticketsService.Add(ticketModel);
            if (result == true)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT: api/Ticket/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TicketModel ticketModel)
        {

            if (id != ticketModel.Id)
            {
                return BadRequest("Ticket ID mismatch");
            }

            var result = await _ticketsService.Update(ticketModel);

            return Ok();
        }

        // DELETE: api/Ticket/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _ticketsService.Delete(id);
            if (result == false)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Ticket not found or delete failed");
            }
        }
    }
}
