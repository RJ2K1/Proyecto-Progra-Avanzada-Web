using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace FrontEnd.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketHelper TicketHelper;
        public TicketController(ITicketHelper ticketHelper) {

            TicketHelper = ticketHelper;
        }
        // GET: TicketController
        public async Task<IActionResult> Index()
        {
            var tickets = await TicketHelper.GetTickets();
            return View(tickets);
        }

        // GET: TicketController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var ticket= await TicketHelper.GetTicket(id);
            if (ticket == null) {
                return NotFound();
            }
            return View(ticket);
        }

        // GET: TicketController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TicketController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TicketViewModel ticketViewModel)
        {
            if (ModelState.IsValid) {
                    var createTicket = await TicketHelper.AddTicket(ticketViewModel);
                if (createTicket != null) {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "No se puedo crear el ticket");
            }
               return View(ticketViewModel);
        }

        // GET: TicketController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
          var ticket = await TicketHelper.GetTicket(id);
            if (ticket == null) {
                return NotFound();
            }
            return View(ticket);    
        }

        // POST: TicketController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TicketViewModel ticketViewModel)
        {
            if (id != ticketViewModel.Id) {
                return NotFound();
            }
            if (ModelState.IsValid) {

                var ActualizarTicket = await TicketHelper.UpdateTicket(ticketViewModel);

                if (ActualizarTicket != null) {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "No se pudo actualizar el ticket");
            }
            return View(ticketViewModel) ;
        }

        // GET: TicketController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TicketController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, IFormCollection collection)
        {
            var success= await TicketHelper.DeleteTicket(id);
            if (success)
            {
                return RedirectToAction(nameof(Index));
               

            }
            ModelState.AddModelError("", "No se pudo eliminar le ticket");
            return View();
        }
    }
}
