using FrontEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontEnd.Helpers.Interfaces
{
    public interface IRolHelper
    {
        Task<List<RolViewModel>> GetRoles();
        Task<RolViewModel> GetRol(int id);
        Task AddRol(RolViewModel rol);
        Task UpdateRol(RolViewModel rol);
        Task DeleteRol(int id);
    }
}
