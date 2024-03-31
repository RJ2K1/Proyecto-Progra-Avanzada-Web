using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public interface IAuthenticationService
{
    Task<bool> RegisterUserAsync(string email, string password);
    Task<bool> LoginUserAsync(string email, string password, HttpContext httpContext);
}
