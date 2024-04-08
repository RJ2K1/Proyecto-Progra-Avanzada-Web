using FrontEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontEnd.Helpers.Interfaces
{
    public interface IDepartamentoHelper
    {
        Task<List<DepartamentoViewModel>> GetDepartamentos();
        Task<DepartamentoViewModel> GetDepartamento(int id);
        Task AddDepartamento(DepartamentoViewModel departamento);
        Task UpdateDepartamento(DepartamentoViewModel departamento);
        Task DeleteDepartamento(int id);
    }
}
