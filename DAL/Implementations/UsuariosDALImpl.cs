using DAL.Interfaces;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class UsuariosDALImpl : DALGenericoImpl<Usuarios>, IUsuariosDAL
    {
        public UsuariosDALImpl(ProyectoWebContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Usuarios>> GetAllIncludingAsync()
        {
            return await _Context.Usuarios
                .Include(u => u.Rol)
                .Include(u => u.Departamento)
                .ToListAsync();
        }
    }
}
