using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

public class AuthenticationService : IAuthenticationService
{
    private readonly ProyectoWebContext _context;
    private readonly ILogger<AuthenticationService> _logger;

    public AuthenticationService(ProyectoWebContext context, ILogger<AuthenticationService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<bool> RegisterUserAsync(string nombre, string email, string password)
    {
        if (_context.Usuarios.Any(u => u.Email == email))
        {
            return false; // El email ya está en uso
        }

        var newUser = new Usuarios
        {
            Nombre = nombre,
            Email = email,
            Contrasena = BCrypt.Net.BCrypt.HashPassword(password),
            RolId = 3 // Asigna el rol por defecto, suponiendo que el rol con ID 3 es 'UsuarioFinal'
        };

        _context.Usuarios.Add(newUser);
        await _context.SaveChangesAsync();
        return true; // Registro exitoso
    }

    public async Task<(bool Success, string Role)> LoginUserAsync(string email, string password, HttpContext httpContext)
    {
        _logger.LogInformation("Buscando usuario con email: {Email}", email);
        var user = await _context.Usuarios.Include(u => u.Rol).SingleOrDefaultAsync(u => u.Email == email);

        if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Contrasena))
        {
            _logger.LogInformation("Usuario encontrado, procediendo a crear claims.");

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Nombre),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Rol.NombreRol),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

            _logger.LogDebug("Claims creadas: {Claims}", claims.Select(c => $"{c.Type}: {c.Value}"));

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1)
            };

            _logger.LogInformation("Autenticando y creando cookie.");
            await httpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            _logger.LogInformation("Cookie de autenticación debería estar creada ahora.");

            return (true, user.Rol.NombreRol);
        }

        _logger.LogWarning("Credenciales incorrectas para el usuario con email: {Email}", email);
        return (false, null);
    }

}
