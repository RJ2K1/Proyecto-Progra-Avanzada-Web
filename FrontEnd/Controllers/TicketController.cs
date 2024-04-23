using Microsoft.AspNetCore.Mvc;
using FrontEnd.Models;
using FrontEnd.Helpers.Interfaces;
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
            return View("ListTicket", tickets);
        }

        // GET: Ticket
        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketHelper.GetTickets();
            return View(tickets);
        }

        // GET: Ticket/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _ticketHelper.GetTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // GET: Ticket/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Ticket/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketViewModel ticketViewModel)
        {
            if (ModelState.IsValid)
            {
                var createdTicket = await _ticketHelper.AddTicket(ticketViewModel);
                if (createdTicket != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Hubo un error al crear el ticket.");
            }
            return View(ticketViewModel);
        }

        // GET: Ticket/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _ticketHelper.GetTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Ticket/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TicketViewModel ticketViewModel)
        {
            if (id != ticketViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var updatedTicket = await _ticketHelper.UpdateTicket(ticketViewModel);
                if (updatedTicket != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Hubo un error al actualizar el ticket.");
            }
            return View(ticketViewModel);
        }

        // GET: Ticket/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _ticketHelper.GetTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Ticket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _ticketHelper.DeleteTicket(id);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Hubo un error al eliminar el ticket.");
            return View();
        }
    }
}
