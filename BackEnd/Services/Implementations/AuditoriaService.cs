// AuditoriaService.cs
using BackEnd.Models;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services.Implementations
{
    public class AuditoriaService : IAuditoriaService
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public AuditoriaService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<List<AuditoriaModel>> GetAuditorias()
        {
            var auditoriasEntity = await _unidadDeTrabajo.AuditoriaDAL.GetAllAsync();
            return auditoriasEntity.Select(a => Convertir(a)).ToList();
        }

        private AuditoriaModel Convertir(Auditoria auditoria)
        {
            return new AuditoriaModel
            {
                Id = auditoria.Id,
                TablaAfectada = auditoria.TablaAfectada,
                RegistroId = auditoria.RegistroId,
                Accion = auditoria.Accion,
                UsuarioId = auditoria.UsuarioId,
                NombreUsuario = auditoria.Usuario.Nombre,  // Aquí asumimos que la entidad Usuario ya está cargada.
                FechaAccion = auditoria.FechaAccion
            };
        }
    }
}
