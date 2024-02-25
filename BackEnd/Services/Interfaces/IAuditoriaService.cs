// IAuditoriaService.cs
using BackEnd.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BackEnd.Services.Interfaces
{
    public interface IAuditoriaService
    {
        Task<List<AuditoriaModel>> GetAuditorias();

    }
}
