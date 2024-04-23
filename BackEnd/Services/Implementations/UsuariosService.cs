using BackEnd.Models;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BackEnd.DTO;
using Microsoft.AspNetCore.Http;

namespace BackEnd.Services.Implementations
{
    public class UsuariosService : IUsuariosService
    {
        private IUnidadDeTrabajo Unidad;
        private readonly IAuditoriaService _auditoriaService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<UsuariosService> _logger;

        public UsuariosService(
            IUnidadDeTrabajo unidadDeTrabajo,
            IAuditoriaService auditoriaService,
            IHttpContextAccessor httpContextAccessor,
            ILogger<UsuariosService> logger)
        {
            Unidad = unidadDeTrabajo;
            _auditoriaService = auditoriaService;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<bool> Add(UsuariosModel usuarioModel)
        {
            var usuario = Convertir(usuarioModel);
            await Unidad.UsuariosDAL.AddAsync(usuario);
            var result = await Unidad.CompleteAsync();
            if (result)
            {
                RegistrarAuditoria("Creación de Usuario", usuario.Id);
            }
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var usuario = await Unidad.UsuariosDAL.GetAsync(id);
            if (usuario == null) return false;

            await Unidad.UsuariosDAL.RemoveAsync(usuario);
            var result = await Unidad.CompleteAsync();
            if (result)
            {
                RegistrarAuditoria("Eliminación de Usuario", id);
            }
            return result;
        }

        public async Task<UsuariosModel> GetById(int id)
        {
            var usuario = await Unidad.UsuariosDAL.GetAsync(id);
            return Convertir(usuario);
        }

        public async Task<List<UsuarioDetalleDto>> GetUsuariosConDetalles()
        {
            var usuariosConDetalles = await Unidad.UsuariosDAL.GetAllIncludingAsync();
            return usuariosConDetalles.Select(u => new UsuarioDetalleDto
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Email = u.Email,
                NombreRol = u.Rol != null ? u.Rol.NombreRol : "Sin Rol",
                NombreDepartamento = u.Departamento != null ? u.Departamento.Nombre : "Sin Departamento"
            }).ToList();
        }

        public async Task<bool> Update(UsuarioUpdateDto usuarioUpdateDto)
        {
            var usuarioExistente = await Unidad.UsuariosDAL.GetAsync(usuarioUpdateDto.Id);
            if (usuarioExistente == null) return false;

            usuarioExistente.Nombre = usuarioUpdateDto.Nombre;
            usuarioExistente.Email = usuarioUpdateDto.Email;
            usuarioExistente.RolId = usuarioUpdateDto.RolId;
            usuarioExistente.DepartamentoId = usuarioUpdateDto.DepartamentoId;

            await Unidad.UsuariosDAL.UpdateAsync(usuarioExistente);
            var result = await Unidad.CompleteAsync();
            if (result)
            {
                RegistrarAuditoria("Actualización de Usuario", usuarioUpdateDto.Id);
            }
            return result;
        }

        private UsuariosModel Convertir(Usuarios usuario)
        {
            return new UsuariosModel
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Contrasena = "",
                RolId = usuario.RolId,
                DepartamentoId = usuario.DepartamentoId
            };
        }

        private Usuarios Convertir(UsuariosModel usuarioModel)
        {
            return new Usuarios
            {
                Id = usuarioModel.Id,
                Nombre = usuarioModel.Nombre,
                Email = usuarioModel.Email,
                Contrasena = HashPassword(usuarioModel.Contrasena),
                RolId = usuarioModel.RolId,
                DepartamentoId = usuarioModel.DepartamentoId
            };
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLowerInvariant();
            }
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
                    TablaAfectada = "Usuarios",
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
