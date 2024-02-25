using DAL.Interfaces;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class AuditoriaDALImpl : DALGenericoImpl<Auditoria>, IAuditoriaDAL
    {
        public AuditoriaDALImpl(ProyectoWebContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Auditoria>> GetAuditoriaByCriteriaAsync(Expression<Func<Auditoria, bool>> criteria)
        {
            return await _Context.Set<Auditoria>().Where(criteria).ToListAsync();
        }
    }
}
