using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Services.Interfaces
{
    public interface IRolesService
    {
        Task<bool> Add(RolesModel rol);
        Task<bool> Delete(int id);
        Task<RolesModel> GetById(int id);
        Task<List<RolesModel>> GetAllRoles();
        Task<bool> Update(RolesModel rol);
    }
}
