using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using FrontEnd.Helpers.Interfaces; // Asegúrate de que este namespace es correcto para IServiceRepository
using FrontEnd.Models;
using FrontEnd.Pages.Account;

namespace FrontEnd.Controllers
{
    public class AccountController : Controller
    {
        private readonly IServiceRepository _serviceRepository;

        public AccountController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(model);
                var response = await _serviceRepository.PostResponse("api/Account/login", json);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    var loginResponse = JsonConvert.DeserializeObject<LoginResponse>(responseContent);

                    if (loginResponse != null)
                    {
                        var claims = new List<Claim>
                {
                    // Aquí se usa Email en lugar de NombreUsuario
                    new Claim(ClaimTypes.Name, loginResponse.Email),
                    new Claim(ClaimTypes.Role, loginResponse.Rol)
                };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties { /* ... */ };
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Usuario o contraseña inválidos.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(model);
                var response = await _serviceRepository.PostResponse("api/Account/register", json);

                if (response.IsSuccessStatusCode)
                {
                    // Puedes redireccionar al usuario a la pantalla de inicio de sesión o a donde sea apropiado
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    // Aquí podrías manejar respuestas HTTP distintas de 200, como un 400 o un 500
                    ModelState.AddModelError(string.Empty, "Ocurrió un error al registrar el usuario.");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }

    public class LoginResponse
    {
        public string Email { get; set; }
        public string Rol { get; set; }
    }
}
