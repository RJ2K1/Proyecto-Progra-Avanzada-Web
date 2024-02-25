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

        public UnidadDeTrabajo(ProyectoWebContext context, IUsuariosDAL usuariosDAL, IRolesDAL rolesDAL)
        {
            _context = context;
            UsuariosDAL = usuariosDAL;
            RolesDAL = rolesDAL;
        }

        public bool Complete()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch (Exception)
            {
                // Manejo de excepciones
                return false;
            }
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}