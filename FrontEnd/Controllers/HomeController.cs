using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
