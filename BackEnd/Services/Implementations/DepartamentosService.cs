using BackEnd.Models;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BackEnd.Services.Implementations
{
    public class DepartamentosService : IDepartamentosService
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;
        private readonly IAuditoriaService _auditoriaService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<DepartamentosService> _logger;

        public DepartamentosService(
            IUnidadDeTrabajo unidadDeTrabajo,
            IAuditoriaService auditoriaService,
            IHttpContextAccessor httpContextAccessor,
            ILogger<DepartamentosService> logger)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
            _auditoriaService = auditoriaService;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<bool> Add(DepartamentosModel departamentomodel)
        {
            var departamento = Convertir(departamentomodel);
            await _unidadDeTrabajo.DepartamentosDAL.AddAsync(departamento);
            var result = await _unidadDeTrabajo.CompleteAsync();
            if (result)
            {
                RegistrarAuditoria("Creación de Departamento", departamento.Id);
            }
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var departamento = await _unidadDeTrabajo.DepartamentosDAL.GetAsync(id);
            if (departamento == null) return false;

            await _unidadDeTrabajo.DepartamentosDAL.RemoveAsync(departamento);
            var result = await _unidadDeTrabajo.CompleteAsync();
            if (result)
            {
                RegistrarAuditoria("Eliminación de Departamento", id);
            }
            return result;
        }

        public async Task<DepartamentosModel> GetById(int id)
        {
            var departamento = await _unidadDeTrabajo.DepartamentosDAL.GetAsync(id);
            return departamento != null ? Convertir(departamento) : null;
        }

        public async Task<List<DepartamentosModel>> GetDepartamentos()
        {
            var departamentos = await _unidadDeTrabajo.DepartamentosDAL.GetAllAsync();
            return departamentos.Select(Convertir).ToList();
        }

        public async Task<bool> Update(DepartamentosModel departamentoModel)
        {
            var departamento = await _unidadDeTrabajo.DepartamentosDAL.GetAsync(departamentoModel.Id);
            if (departamento == null) return false;

            departamento.Nombre = departamentoModel.NombreDepartamento;
            departamento.Descripcion = departamentoModel.Descripcion;
            await _unidadDeTrabajo.DepartamentosDAL.UpdateAsync(departamento);
            var result = await _unidadDeTrabajo.CompleteAsync();
            if (result)
            {
                RegistrarAuditoria("Actualización de Departamento", departamentoModel.Id);
            }
            return result;
        }

        private DepartamentosModel Convertir(Departamentos departamento)
        {
            return new DepartamentosModel
            {
                Id = departamento.Id,
                NombreDepartamento = departamento.Nombre,
                Descripcion = departamento.Descripcion
            };
        }

        private Departamentos Convertir(DepartamentosModel departamentoModel)
        {
            return new Departamentos
            {
                Id = departamentoModel.Id,
                Nombre = departamentoModel.NombreDepartamento,
                Descripcion = departamentoModel.Descripcion
            };
        }

        private async void RegistrarAuditoria(string accion, int registroId)
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                string userIdValue = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                int userId = int.TryParse(userIdValue, out var uid) ? uid : 0; // Handle default or error case as needed

                await _auditoriaService.Add(new AuditoriaModel
                {
                    Accion = accion,
                    FechaAccion = DateTime.UtcNow,
                    RegistroId = registroId,
                    TablaAfectada = "Departamentos",
                    UsuarioId = userId
                });
            }
            else
            {
                _logger.LogWarning("Intento de registrar auditoría sin usuario autenticado.");
            }
        }
    }
}
