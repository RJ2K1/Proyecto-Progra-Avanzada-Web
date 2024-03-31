using Microsoft.AspNetCore.Mvc;
using FrontEnd.Models;
using FrontEnd.Helpers.Interfaces;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioHelper _usuarioHelper;

        public UsuarioController(IUsuarioHelper usuarioHelper)
        {
            _usuarioHelper = usuarioHelper;
        }

        // GET: Usuario
        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioHelper.GetUsuarios();
            return View(usuarios);
        }

        // GET: Usuario/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var usuario = await _usuarioHelper.GetUsuario(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioViewModel usuarioViewModel)
        {
            if (ModelState.IsValid)
            {
                var createdUsuario = await _usuarioHelper.AddUsuario(usuarioViewModel);
                if (createdUsuario != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Hubo un error al crear el usuario.");
            }
            return View(usuarioViewModel);
        }

        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var usuario = await _usuarioHelper.GetUsuario(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UsuarioViewModel usuarioViewModel)
        {
            if (id != usuarioViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var updatedUsuario = await _usuarioHelper.UpdateUsuario(usuarioViewModel);
                if (updatedUsuario != null)
                {
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("", "Hubo un error al actualizar el usuario.");
            }
            return View(usuarioViewModel);
        }

        // GET: Usuario/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _usuarioHelper.GetUsuario(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var success = await _usuarioHelper.DeleteUsuario(id);
            if (success)
            {
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "Hubo un error al eliminar el usuario.");
            return View();
        }
    }
}
