using DAL.Interfaces;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class AuditoriaDALImpl : DALGenericoImpl<Auditoria>, IAuditoriaDAL
    {
        private readonly ProyectoWebContext _context;

        public AuditoriaDALImpl(ProyectoWebContext context) : base(context)
        {
            _context = context;
        }


        public async Task<List<Auditoria>> GetAuditoriasConUsuario()
        {
            return await _context.Auditoria.Include(a => a.Usuario).ToListAsync();
        }
    }
}
