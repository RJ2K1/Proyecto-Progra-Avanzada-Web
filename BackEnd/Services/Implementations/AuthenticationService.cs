using Entities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using BCrypt.Net;

public class AuthenticationService : IAuthenticationService
{
    private readonly ProyectoWebContext _context;

    public AuthenticationService(ProyectoWebContext context)
    {
        _context = context;
    }

    public async Task<bool> RegisterUserAsync(string email, string password)
    {
        if (_context.Usuarios.Any(u => u.Email == email))
        {
            // El email ya está en uso
            return false;
        }

        var defaultRole = await _context.Roles.FirstOrDefaultAsync(r => r.NombreRol == "UsuarioFinal");
        if (defaultRole == null)
        {
            // No se encuentra el rol "UsuarioFinal"
            return false;
        }

        var newUser = new Usuarios
        {
            Email = email,
            Contrasena = BCrypt.Net.BCrypt.HashPassword(password),
            RolId = defaultRole.Id // Asigna el rol "UsuarioFinal" por defecto
        };

        _context.Usuarios.Add(newUser);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> LoginUserAsync(string email, string password, HttpContext httpContext)
    {
        var user = await _context.Usuarios
                                 .Include(u => u.Rol) // Asegúrate de incluir el rol para acceso posterior
                                 .SingleOrDefaultAsync(u => u.Email == email);

        if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Contrasena))
        {
            // Crear y configurar la cookie con el rol del usuario
            var cookieOptions = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(1),
                IsEssential = true // Esto indica que la cookie es esencial para el funcionamiento de la aplicación
            };

            httpContext.Response.Cookies.Append("UserRole", user.Rol.NombreRol, cookieOptions);
            return true;
        }

        return false;
    }
}
