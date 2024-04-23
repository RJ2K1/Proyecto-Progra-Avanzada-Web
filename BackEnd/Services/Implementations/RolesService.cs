using BackEnd.Models;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;



namespace BackEnd.Services.Implementations
{
    public class RolesService : IRolesService
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IAuditoriaService _auditoriaService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<RolesService> _logger;

        public RolesService(
            IUnidadDeTrabajo unidadDeTrabajo,
            IAuditoriaService auditoriaService,
            IHttpContextAccessor httpContextAccessor,
            ILogger<RolesService> logger) // Aseg�rate de que el tipo gen�rico es la clase actual
        {
            _unidadDeTrabajo = unidadDeTrabajo;
            _auditoriaService = auditoriaService;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger; // La variable _logger debe coincidir con el nombre del par�metro
        }

        public async Task<bool> Add(RolesModel rolModel)
        {
            var rol = new Roles { NombreRol = rolModel.NombreRol };
            await _unidadDeTrabajo.RolesDAL.AddAsync(rol);
            var result = await _unidadDeTrabajo.CompleteAsync();

            if (result)
            {
 
                RegistrarAuditoria("Creaci�n de Rol", rol.Id);
            }

            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var rol = await _unidadDeTrabajo.RolesDAL.GetAsync(id);
            if (rol == null) return false;
            await _unidadDeTrabajo.RolesDAL.RemoveAsync(rol);
            var result = await _unidadDeTrabajo.CompleteAsync();
            if (result)
            {
                RegistrarAuditoria("Eliminaci�n de Rol", rol.Id);
            }
            return result;
        }

        public async Task<RolesModel> GetById(int id)
        {
            // Este m�todo no modifica datos, por lo que no hay necesidad de registrar en auditor�a
            var rol = await _unidadDeTrabajo.RolesDAL.GetAsync(id);
            if (rol == null) return null;
            return new RolesModel
            {
                Id = rol.Id,
                NombreRol = rol.NombreRol
            };
        }

        public async Task<List<RolesModel>> GetAllRoles()
        {
            // Este m�todo no modifica datos, por lo que no hay necesidad de registrar en auditor�a
            var roles = await _unidadDeTrabajo.RolesDAL.GetAllAsync();
            return roles.Select(rol => new RolesModel
            {
                Id = rol.Id,
                NombreRol = rol.NombreRol
            }).ToList();
        }

        public async Task<bool> Update(RolesModel rolModel)
        {
            var rol = await _unidadDeTrabajo.RolesDAL.GetAsync(rolModel.Id);
            if (rol == null) return false;
            rol.NombreRol = rolModel.NombreRol;
            var result = await _unidadDeTrabajo.RolesDAL.UpdateAsync(rol);
            if (result)
            {
                RegistrarAuditoria("Actualizaci�n de Rol", rolModel.Id);
            }
            return result;
        }

        private async void RegistrarAuditoria(string accion, int registroId)
        {
            // Suponiendo que el usuario est� autenticado y el ID est� disponible
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                string userIdValue = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                int userId = int.Parse(userIdValue);

                await _auditoriaService.Add(new AuditoriaModel
                {
                    Accion = accion,
                    FechaAccion = DateTime.UtcNow,
                    RegistroId = registroId,
                    TablaAfectada = "Roles",
                    UsuarioId = userId
                });
            }
            else
            {
                _logger.LogWarning("Intento de registrar auditor�a sin usuario autenticado.");
            }
        }
    }
}
