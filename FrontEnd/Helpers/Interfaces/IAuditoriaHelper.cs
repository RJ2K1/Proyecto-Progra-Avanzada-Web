using FrontEnd.ApiModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrontEnd.Helpers.Interfaces
{
    public interface IAuditoriaHelper
    {
        Task<List<Auditoria>> GetAuditorias();
    }
}
