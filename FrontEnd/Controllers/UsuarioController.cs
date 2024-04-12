using Microsoft.AspNetCore.Mvc;
using FrontEnd.Models;
using FrontEnd.Helpers.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering; // Necesario para usar SelectList
using FrontEnd.Helpers.Implementations;
using Microsoft.Extensions.Logging;

namespace FrontEnd.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioHelper _usuarioHelper;
        private readonly IRolHelper _rolHelper; // Asumiendo que tienes un helper para roles
        private readonly IDepartamentoHelper _departamentoHelper; // Asumiendo que tienes un helper para departamentos
        private readonly ILogger<UsuarioController> _logger;
        public UsuarioController(IUsuarioHelper usuarioHelper, IRolHelper rolHelper, IDepartamentoHelper departamentoHelper, ILogger<UsuarioController> logger)
        {
            _usuarioHelper = usuarioHelper;
            _rolHelper = rolHelper;
            _departamentoHelper = departamentoHelper;
            _logger = logger; // Aquí estaba el error, debe ser _logger en lugar de logger.
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
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = new SelectList(await _rolHelper.GetRoles(), "Id", "NombreRol");
            ViewBag.Departamentos = new SelectList(await _departamentoHelper.GetDepartamentos(), "Id", "NombreDepartamento");
            return View(new UsuarioCreateViewModel());
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioCreateViewModel usuarioCreateViewModel)
        {
            _logger.LogInformation("Iniciando el proceso de creación de un nuevo usuario");

            if (ModelState.IsValid)
            {
                var creationSuccess = await _usuarioHelper.AddUsuario(usuarioCreateViewModel);
                if (creationSuccess)
                {
                    _logger.LogInformation("Usuario creado exitosamente. Redirigiendo al índice.");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    _logger.LogError("No se pudo crear el usuario. Error en el lado del servidor.");
                    ModelState.AddModelError("", "Hubo un error al crear el usuario.");
                }
            }
            else
            {
                _logger.LogWarning("ModelState no es válido. Error(es): {ModelStateErrors}", string.Join("; ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage)));
            }

            ViewBag.Roles = new SelectList(await _rolHelper.GetRoles(), "Id", "NombreRol");
            ViewBag.Departamentos = new SelectList(await _departamentoHelper.GetDepartamentos(), "Id", "NombreDepartamento");
            return View(usuarioCreateViewModel);
        }




        // GET: Usuario/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var usuarioViewModel = await _usuarioHelper.GetUsuario(id);
            if (usuarioViewModel == null)
            {
                return NotFound();
            }

            var usuarioUpdateViewModel = new UsuarioUpdateViewModel
            {
                Id = usuarioViewModel.Id,
                Nombre = usuarioViewModel.Nombre,
                Email = usuarioViewModel.Email,
                RolId = usuarioViewModel.RolId,
                DepartamentoId = usuarioViewModel.DepartamentoId
            };

            // Preparar ViewBag para dropdowns
            ViewBag.Roles = new SelectList(await _rolHelper.GetRoles(), "Id", "NombreRol", usuarioViewModel.RolId);
            ViewBag.Departamentos = new SelectList(await _departamentoHelper.GetDepartamentos(), "Id", "NombreDepartamento", usuarioViewModel.DepartamentoId);

            return View(usuarioUpdateViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UsuarioUpdateViewModel usuarioUpdateViewModel)
        {
            _logger.LogInformation("Inicio del proceso de actualización para el usuario con ID: {UserId}", id);

            if (id != usuarioUpdateViewModel.Id)
            {
                _logger.LogWarning("Conflicto de ID: El parámetro ID {ParamId} no coincide con el ID del modelo {ModelId}", id, usuarioUpdateViewModel.Id);
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("El modelo para el usuario con ID: {UserId} no es válido", id);
                ViewBag.Roles = new SelectList(await _rolHelper.GetRoles(), "Id", "NombreRol", usuarioUpdateViewModel.RolId);
                ViewBag.Departamentos = new SelectList(await _departamentoHelper.GetDepartamentos(), "Id", "NombreDepartamento", usuarioUpdateViewModel.DepartamentoId);
                return View(usuarioUpdateViewModel);
            }

            bool updated = await _usuarioHelper.UpdateUsuario(usuarioUpdateViewModel);
            if (updated)
            {
                _logger.LogInformation("Usuario con ID: {UserId} actualizado correctamente", id);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                _logger.LogError("Error al intentar actualizar el usuario con ID: {UserId}", id);
                ModelState.AddModelError("", "Hubo un error al intentar actualizar el usuario.");
                ViewBag.Roles = new SelectList(await _rolHelper.GetRoles(), "Id", "NombreRol", usuarioUpdateViewModel.RolId);
                ViewBag.Departamentos = new SelectList(await _departamentoHelper.GetDepartamentos(), "Id", "NombreDepartamento", usuarioUpdateViewModel.DepartamentoId);
                return View(usuarioUpdateViewModel);
            }
        }





        // GET: Usuario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var usuario = await _usuarioHelper.DeleteUsuario(id);
            await _usuarioHelper.DeleteUsuario(id);
            return RedirectToAction(nameof(Index));
        }




    }
}
