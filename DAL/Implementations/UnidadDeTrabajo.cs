using DAL.Interfaces;
using Entities.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo, IDisposable
    {

        private readonly ProyectoWebContext _context;
        private readonly ILogger<UnidadDeTrabajo> _logger;

        public IUsuariosDAL UsuariosDAL { get; private set; }
        public IRolesDAL RolesDAL { get; private set; }
        public ITicketsDAL TicketsDAL { get; private set; }
        public IDepartamentosDAL DepartamentosDAL { get; private set; }
        public IAuditoriaDAL AuditoriaDAL { get; private set; }

        public UnidadDeTrabajo(ProyectoWebContext context, 
                               IUsuariosDAL usuariosDAL, 
                               IRolesDAL rolesDAL, 
                               ITicketsDAL ticketsDAL, 
                               IDepartamentosDAL departamentosDAL, 
                               IAuditoriaDAL auditoriaDAL,
                               ILogger<UnidadDeTrabajo> logger)
        {
            _context = context;
            UsuariosDAL = usuariosDAL;
            RolesDAL = rolesDAL;
            TicketsDAL = ticketsDAL;
            DepartamentosDAL = departamentosDAL;
            AuditoriaDAL = auditoriaDAL;
            _logger = logger;
        }

        public async Task<bool> CompleteAsync()
        {
            try
            {
                var changes = await _context.SaveChangesAsync();
                return changes > 0; // Esto debería devolver 'true' si hay cambios confirmados.
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió un error al guardar los cambios en la base de datos.");
                return false; // Esto debería devolver 'false' solo si hay una excepción.
            }
        }


        public bool Complete()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
               
                _logger.LogError(ex, "Error al completar la operación en la base de datos.");
                return false;
            }
        }


        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
