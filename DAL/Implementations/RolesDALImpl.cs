using DAL.Interfaces;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations
{
    public class RolesDALImpl : DALGenericoImpl<Roles>, IRolesDAL
    {
        public RolesDALImpl(ProyectoWebContext context) : base(context)
        {
        }

    }
}