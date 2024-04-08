using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using BackEnd.DTO; 

namespace BackEnd.Services.Interfaces
{
    public interface IUsuariosService
    {
        Task<bool> Add(UsuariosModel usuario);
        Task<bool> Delete(int id);
        Task<UsuariosModel> GetById(int id);
        Task<bool> Update(UsuarioUpdateDto usuarioUpdateDto);
        Task<List<UsuarioDetalleDto>> GetUsuariosConDetalles(); 
    }
}
