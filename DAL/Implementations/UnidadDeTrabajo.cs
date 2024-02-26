using DAL.Interfaces;
using Entities.Entities;
using System;

namespace DAL.Implementations
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {
        private readonly ProyectoWebContext _context;

        public IUsuariosDAL UsuariosDAL { get; private set; }
        public IAuditoriaDAL AuditoriaDAL { get; private set; }
        public IDepartamentosDAL DepartamentosDAL   { get; private set; }

        public UnidadDeTrabajo(ProyectoWebContext context, IUsuariosDAL usuariosDAL, IAuditoriaDAL AuditoriaDAL, IDepartamentosDAL departamentosDAL)
        {
            _context = context;
            UsuariosDAL = usuariosDAL;
            AuditoriaDAL = auditoriaDAL;
            DepartamentosDAL = departamentosDAL;
        }

        public bool Complete()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
               //Manejo de excepciones
                return false;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
