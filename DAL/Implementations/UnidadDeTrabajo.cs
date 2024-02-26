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
        public IRolesDAL RolesDAL { get; private set; }
        public IDepartamentosDAL DepartamentosDAL { get; private set; }
        public ITicketsDAL TicketsDAL { get; private set; }

       
        public UnidadDeTrabajo(ProyectoWebContext context, IUsuariosDAL usuariosDAL, IAuditoriaDAL auditoriaDAL, IRolesDAL rolesDAL, IDepartamentosDAL departamentosDAL, ITicketsDAL ticketsDAL)
        {
            _context = context;
            UsuariosDAL = usuariosDAL;
            AuditoriaDAL = auditoriaDAL;
            RolesDAL = rolesDAL;
            DepartamentosDAL = departamentosDAL;
            TicketsDAL = ticketsDAL;
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
            _context.Dispose();
        }
    }
}
