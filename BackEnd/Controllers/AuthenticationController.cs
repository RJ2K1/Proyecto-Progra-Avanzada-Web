using Microsoft.AspNetCore.Mvc;
using BackEnd.Models;
using BackEnd.Services.Interfaces;
using System.Threading.Tasks;
using System;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        bool result = await _authenticationService.RegisterUserAsync(model.Nombre, model.Email, model.Password);
        if (result)
        {
            return Ok(new { message = "Usuario registrado con éxito." });
        }
        else
        {
            return BadRequest(new { message = "El email ya está en uso." });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var (success, role) = await _authenticationService.LoginUserAsync(model.Email, model.Password, HttpContext);

        if (success)
        {
            return Ok(new { message = "Inicio de sesión exitoso.", role });
        }
        else
        {
            return BadRequest(new { message = "Credenciales incorrectas." });
        }
    }
}
