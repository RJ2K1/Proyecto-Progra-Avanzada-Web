using DAL.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class DepartamentosDALImpl : DALGenericoImpl<Departamentos>, IDepartamentosDAL
    {
        public DepartamentosDALImpl(ProyectoWebContext context) : base(context)
        {

        }
        
    }
}