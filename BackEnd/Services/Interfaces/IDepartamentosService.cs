using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Services.Interfaces
{
    public interface IDepartamentosService
    {
        Task<bool> Add(DepartamentosModel departamento);
        Task<bool> Delete(int id);
        Task<DepartamentosModel> GetById(int id);
        Task<List<DepartamentosModel>> GetDepartamentos();
        Task<bool> Update(DepartamentosModel departamento);
    }
}
