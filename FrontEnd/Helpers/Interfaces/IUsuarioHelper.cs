using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface IUsuarioHelper
    {
        Task<List<UsuarioViewModel>> GetUsuarios();
        Task<UsuarioViewModel> GetUsuario(int id);
        Task<bool> AddUsuario(UsuarioCreateViewModel usuario);
        Task<bool> DeleteUsuario(int id);
        Task<bool> UpdateUsuario(UsuarioUpdateViewModel usuario); // Actualizado para usar UsuarioUpdateViewModel
    }
}
