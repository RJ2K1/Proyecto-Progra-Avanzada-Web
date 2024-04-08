using Entities.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUsuariosDAL : IDALGenerico<Usuarios>
    {
        Task<IEnumerable<Usuarios>> GetAllIncludingAsync();
    }
}
