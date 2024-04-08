using Microsoft.AspNetCore.Mvc;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    public class RolController : Controller
    {
        private readonly IRolHelper _rolHelper;

        public RolController(IRolHelper rolHelper)
        {
            _rolHelper = rolHelper;
        }

        // GET: Rol
        public async Task<IActionResult> Index()
        {
            var roles = await _rolHelper.GetRoles();
            return View(roles);
        }

        // GET: Rol/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var rol = await _rolHelper.GetRol(id);
            if (rol == null)
            {
                return NotFound();
            }
            return View(rol);
        }

        // GET: Rol/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rol/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RolViewModel rolViewModel)
        {
            if (ModelState.IsValid)
            {
                await _rolHelper.AddRol(rolViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(rolViewModel);
        }

        // GET: Rol/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var rol = await _rolHelper.GetRol(id);
            if (rol == null)
            {
                return NotFound();
            }
            return View(rol);
        }

        // POST: Rol/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, RolViewModel rolViewModel)
        {
            if (id != rolViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _rolHelper.UpdateRol(rolViewModel);
                return RedirectToAction(nameof(Index));
            }
            return View(rolViewModel);
        }

        // GET: Rol/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var rol = await _rolHelper.GetRol(id);
            if (rol == null)
            {
                return NotFound();
            }
            return View(rol);
        }

        // POST: Rol/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _rolHelper.DeleteRol(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
