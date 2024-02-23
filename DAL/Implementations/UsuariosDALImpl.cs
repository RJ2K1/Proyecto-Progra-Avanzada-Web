using DAL.Interfaces;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Implementations
{
    public class UsuariosDALImpl : DALGenericoImpl<Usuarios>, IUsuariosDAL
    {
        public UsuariosDALImpl(ProyectoWebContext context) : base(context)
        {
        }

    }
}
