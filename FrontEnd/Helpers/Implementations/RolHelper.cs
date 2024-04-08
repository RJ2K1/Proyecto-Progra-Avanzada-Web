using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Helpers.Implementations
{
    public class RolHelper : IRolHelper
    {
        private readonly IServiceRepository _repository;

        public RolHelper(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<RolViewModel>> GetRoles()
        {
            var responseMessage = await _repository.GetResponse("api/Roles");
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var roles = JsonConvert.DeserializeObject<List<Rol>>(content);
                return roles.ConvertAll(r => Convertir(r));
            }
            return new List<RolViewModel>();
        }

        public async Task<RolViewModel> GetRol(int id)
        {
            var responseMessage = await _repository.GetResponse($"api/Roles/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var rol = JsonConvert.DeserializeObject<Rol>(content);
                return Convertir(rol);
            }
            return null;
        }

        public async Task AddRol(RolViewModel rolViewModel)
        {
            var rolApi = Convertir(rolViewModel);
            var content = new StringContent(JsonConvert.SerializeObject(rolApi), Encoding.UTF8, "application/json");
            await _repository.PostResponse("api/Roles", content);
        }

        public async Task UpdateRol(RolViewModel rolViewModel)
        {
            var rolApi = Convertir(rolViewModel);
            var content = new StringContent(JsonConvert.SerializeObject(rolApi), Encoding.UTF8, "application/json");
            await _repository.PutResponse($"api/Roles/{rolViewModel.Id}", content);
        }

        public async Task DeleteRol(int id)
        {
            await _repository.DeleteResponse($"api/Roles/{id}");
        }

        private Rol Convertir(RolViewModel rolViewModel)
        {
            return new Rol
            {
                Id = rolViewModel.Id,
                NombreRol = rolViewModel.NombreRol
            };
        }

        private RolViewModel Convertir(Rol rol)
        {
            return new RolViewModel
            {
                Id = rol.Id,
                NombreRol = rol.NombreRol
            };
        }
    }
}
