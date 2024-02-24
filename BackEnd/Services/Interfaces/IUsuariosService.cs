using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Services.Interfaces
{
    public interface IUsuariosService
    {
        Task<bool> Add(UsuariosModel usuario);
        Task<bool> Delete(int id);
        Task<UsuariosModel> GetById(int id);
        Task<List<UsuariosModel>> GetUsuarios();
        Task<bool> Update(UsuariosModel usuario);
    }
}
