using Microsoft.AspNetCore.Mvc;
using FrontEnd.Helpers.Interfaces;
using System.Threading.Tasks;

namespace FrontEnd.Controllers
{
    public class AuditoriaController : Controller
    {
        private readonly IAuditoriaHelper _auditoriaHelper;

        public AuditoriaController(IAuditoriaHelper auditoriaHelper)
        {
            _auditoriaHelper = auditoriaHelper;
        }

        // GET: Auditoria
        public async Task<IActionResult> Index()
        {
            var auditorias = await _auditoriaHelper.GetAuditorias();
            return View(auditorias);
        }
    }
}
