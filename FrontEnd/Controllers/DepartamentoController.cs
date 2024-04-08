using Microsoft.AspNetCore.Mvc;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    public class DepartamentoController : Controller
    {
        private readonly IDepartamentoHelper _departamentoHelper;

        public DepartamentoController(IDepartamentoHelper departamentoHelper)
        {
            _departamentoHelper = departamentoHelper;
        }

        public async Task<IActionResult> Index()
        {
            var departamentos = await _departamentoHelper.GetDepartamentos();
            return View(departamentos);
        }

        public async Task<IActionResult> Details(int id)
        {
            var departamento = await _departamentoHelper.GetDepartamento(id);
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartamentoViewModel departamento)
        {
            if (ModelState.IsValid)
            {
                await _departamentoHelper.AddDepartamento(departamento);
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var departamento = await _departamentoHelper.GetDepartamento(id);
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DepartamentoViewModel departamento)
        {
            if (id != departamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _departamentoHelper.UpdateDepartamento(departamento);
                return RedirectToAction(nameof(Index));
            }
            return View(departamento);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var departamento = await _departamentoHelper.GetDepartamento(id);
            if (departamento == null)
            {
                return NotFound();
            }
            return View(departamento);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _departamentoHelper.DeleteDepartamento(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
