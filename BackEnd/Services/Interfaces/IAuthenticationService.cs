using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public interface IAuthenticationService
{
    Task<bool> RegisterUserAsync(string nombre, string email, string password);
    Task<(bool Success, string Role)> LoginUserAsync(string email, string password, HttpContext httpContext);
}
