using BackEnd.Models;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BackEnd.DTO;

namespace BackEnd.Services.Implementations
{
    public class UsuariosService : IUsuariosService
    {
        private IUnidadDeTrabajo Unidad;

        public UsuariosService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            Unidad = unidadDeTrabajo;
        }

        public async Task<bool> Add(UsuariosModel usuarioModel)
        {
            var usuario = Convertir(usuarioModel);
            await Unidad.UsuariosDAL.AddAsync(usuario); 
            var result = Unidad.Complete(); 
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var usuario = new Usuarios { Id = id };
            await Unidad.UsuariosDAL.RemoveAsync(usuario); 
            var result = Unidad.Complete(); 
            return result;
        }

        public async Task<UsuariosModel> GetById(int id)
        {
            var usuario = await Unidad.UsuariosDAL.GetAsync(id); 
            return Convertir(usuario);
        }

        public async Task<List<UsuarioDetalleDto>> GetUsuariosConDetalles()
        {
            var usuariosConDetalles = await Unidad.UsuariosDAL.GetAllIncludingAsync(); // Asumiendo que este método hace lo que su nombre indica
            return usuariosConDetalles.Select(u => new UsuarioDetalleDto
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Email = u.Email,
                NombreRol = u.Rol != null ? u.Rol.NombreRol : "Sin Rol", // Proporciona "Sin Rol" si u.Rol es nulo
                NombreDepartamento = u.Departamento != null ? u.Departamento.Nombre : "Sin Departamento" // Proporciona "Sin Departamento" si u.Departamento es nulo
            }).ToList();
        }


        public async Task<bool> Update(UsuarioUpdateDto usuarioUpdateDto)
        {
            var usuarioExistente = await Unidad.UsuariosDAL.GetAsync(usuarioUpdateDto.Id);
            if (usuarioExistente == null)
            {
                // Handle the case where the user doesn't exist.
                return false;
            }

            // Update the fields from the DTO, except for the ID.
            usuarioExistente.Nombre = usuarioUpdateDto.Nombre;
            usuarioExistente.Email = usuarioUpdateDto.Email;
            usuarioExistente.RolId = usuarioUpdateDto.RolId;
            usuarioExistente.DepartamentoId = usuarioUpdateDto.DepartamentoId;

            // Save the changes.
            await Unidad.UsuariosDAL.UpdateAsync(usuarioExistente);
            return Unidad.Complete();
        }



        private UsuariosModel Convertir(Usuarios usuario)
        {
           
            return new UsuariosModel
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                Contrasena = "", // No devolver la contraseña
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
                Contrasena = HashPassword(usuarioModel.Contrasena), // Hashear la contraseña antes de guardar
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
    }
}
