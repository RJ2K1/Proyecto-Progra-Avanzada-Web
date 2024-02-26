using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Services.Interfaces
{
    public interface IAuditoriaService
    {
        Task<bool> Add(AuditoriaModel auditoria);
        Task<bool> Delete(int id);
        Task<AuditoriaModel> GetById(int id);
        Task<List<AuditoriaModel>> GetAuditorias();
        Task<bool> Update(AuditoriaModel auditoria);
    }
}