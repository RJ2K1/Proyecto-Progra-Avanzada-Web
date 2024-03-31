using FrontEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontEnd.Helpers.Interfaces
{
    public interface IUsuarioHelper
    {
        Task<List<UsuarioViewModel>> GetUsuarios();
        Task<UsuarioViewModel> GetUsuario(int id);
        Task<UsuarioViewModel> AddUsuario(UsuarioViewModel usuario);
        Task<bool> DeleteUsuario(int id); // Cambiado a Task<bool>
        Task<UsuarioViewModel> UpdateUsuario(UsuarioViewModel usuario);
    }
}
