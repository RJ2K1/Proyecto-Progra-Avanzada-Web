using BackEnd.Models;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services.Implementations
{
    public class RolesService : IRolesService
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public RolesService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<bool> Add(RolesModel rolModel)
        {
            var rol = new Roles { NombreRol = rolModel.NombreRol };
            await _unidadDeTrabajo.RolesDAL.AddAsync(rol);
            return _unidadDeTrabajo.Complete(); 
        }

        public async Task<bool> Delete(int id)
        {
            var rol = new Roles { Id = id }; 
            await _unidadDeTrabajo.RolesDAL.RemoveAsync(rol);
            return _unidadDeTrabajo.Complete(); 
        }

        public async Task<RolesModel> GetById(int id)
        {
            var rol = await _unidadDeTrabajo.RolesDAL.GetAsync(id);
            return new RolesModel
            {
                Id = rol.Id,
                NombreRol = rol.NombreRol
            };
        }

        public async Task<List<RolesModel>> GetAllRoles()
        {
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
            await _unidadDeTrabajo.RolesDAL.UpdateAsync(rol); 
            return _unidadDeTrabajo.Complete();
        }
    }
}
