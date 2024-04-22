using Microsoft.AspNetCore.Mvc;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketHelper _ticketHelper;

        public TicketController(ITicketHelper ticketHelper)
        {
            _ticketHelper = ticketHelper;
        }

        public async Task<IActionResult> ListTicket()
        {
            var tickets = await _ticketHelper.GetTickets();
            return View("ListTicket", tickets); ;
        }

        // GET: Ticket
        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketHelper.GetTickets();
            return View(tickets);
        }

        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _ticketHelper.GetTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketViewModel ticket)
        {
            if (ModelState.IsValid)
            {
                await _ticketHelper.AddTicket(ticket);
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _ticketHelper.GetTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TicketViewModel ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _ticketHelper.UpdateTicket(ticket);
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _ticketHelper.GetTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ticketHelper.DeleteTicket(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
