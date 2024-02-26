using DAL.Interfaces;
using Entities.Entities;
using System;

namespace DAL.Implementations
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {
        private readonly ProyectoWebContext _context;

        public IUsuariosDAL UsuariosDAL { get; private set; }
        public IRolesDAL RolesDAL { get; private set; }
        public ITicketsDAL _ticketsDAL { get; private set; } 
        public IDepartamentosDAL DepartamentosDAL { get; private set; }
        public IAuditoriaDAL AuditoriaDAL { get; private set; }

      
        public UnidadDeTrabajo(ProyectoWebContext context, IUsuariosDAL usuariosDAL, IRolesDAL rolesDAL, ITicketsDAL ticketsDAL, IDepartamentosDAL departamentosDAL, IAuditoriaDAL auditoriaDAL)
        {
            _context = context;
            UsuariosDAL = usuariosDAL;
            RolesDAL = rolesDAL;
            _ticketsDAL = ticketsDAL;
            DepartamentosDAL = departamentosDAL;
            AuditoriaDAL = auditoriaDAL;
        }

        public bool Complete()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Dispose()
        {
      
        }
    }
}
