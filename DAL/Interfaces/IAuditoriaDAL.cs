using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Entities.Entities;

namespace DAL.Interfaces
{
    public interface IAuditoriaDAL : IDALGenerico<Auditoria>
    {

         Task<List<Auditoria>> GetAuditoriasConUsuario();

    }
}
