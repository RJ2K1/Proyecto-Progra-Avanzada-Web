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

        public async Task<bool> Add(AuditoriaModel auditoriaModel)
        {
            var auditoria = ConvertirAEntidad(auditoriaModel);
            await _unidadDeTrabajo.AuditoriaDAL.AddAsync(auditoria);
            return _unidadDeTrabajo.Complete();
        }

        public async Task<bool> Delete(int id)
        {
            await _unidadDeTrabajo.AuditoriaDAL.RemoveAsync(new Auditoria { Id = id });
            return _unidadDeTrabajo.Complete();
        }

        public async Task<AuditoriaModel> GetById(int id)
        {
            var auditoria = await _unidadDeTrabajo.AuditoriaDAL.GetAsync(id);
            return auditoria != null ? ConvertirAModelo(auditoria) : null;
        }

        public async Task<List<AuditoriaModel>> GetAuditorias()
        {
            var auditorias = await _unidadDeTrabajo.AuditoriaDAL.GetAuditoriasConUsuario();
            return auditorias.Select(a => ConvertirAModelo(a)).ToList();
        }

        public async Task<bool> Update(AuditoriaModel auditoriaModel)
        {
            var auditoria = ConvertirAEntidad(auditoriaModel);
            await _unidadDeTrabajo.AuditoriaDAL.UpdateAsync(auditoria);
            return _unidadDeTrabajo.Complete();
        }

        private AuditoriaModel ConvertirAModelo(Auditoria auditoria)
        {
            return new AuditoriaModel
            {
                Id = auditoria.Id,
                TablaAfectada = auditoria.TablaAfectada,
                RegistroId = auditoria.RegistroId,
                Accion = auditoria.Accion,
                UsuarioId = auditoria.UsuarioId,
                NombreUsuario = auditoria.Usuario?.Nombre ?? "Nombre no disponible",
                FechaAccion = auditoria.FechaAccion
            };
        }

        private Auditoria ConvertirAEntidad(AuditoriaModel auditoriaModel)
        {
            return new Auditoria
            {
                Id = auditoriaModel.Id,
                TablaAfectada = auditoriaModel.TablaAfectada,
                RegistroId = auditoriaModel.RegistroId,
                Accion = auditoriaModel.Accion,
                UsuarioId = auditoriaModel.UsuarioId,
                FechaAccion = auditoriaModel.FechaAccion
            };
        }
    }
}
