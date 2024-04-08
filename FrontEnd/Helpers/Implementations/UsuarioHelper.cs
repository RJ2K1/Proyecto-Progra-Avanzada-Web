using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEnd.Helpers.Implementations
{
    public class UsuarioHelper : IUsuarioHelper
    {
        private readonly IServiceRepository _repository;

        public UsuarioHelper(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<UsuarioViewModel> AddUsuario(UsuarioViewModel usuarioViewModel)
        {
            var usuarioApi = Convertir(usuarioViewModel);
            var responseMessage = await _repository.PostResponse("api/Usuarios", usuarioApi);
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var addedUsuario = JsonConvert.DeserializeObject<Usuario>(content);
                return Convertir(addedUsuario);
            }
            return null;
        }

        public async Task<bool> DeleteUsuario(int id)
        {
            var responseMessage = await _repository.DeleteResponse($"api/Usuarios/{id}");
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<List<UsuarioViewModel>> GetUsuarios()
        {
            var responseMessage = await _repository.GetResponse("api/usuarios");
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var usuarios = JsonConvert.DeserializeObject<List<Usuario>>(content);
                return usuarios.ConvertAll(u => Convertir(u));
            }
            return new List<UsuarioViewModel>();
        }

        public async Task<UsuarioViewModel> GetUsuario(int id)
        {
            var responseMessage = await _repository.GetResponse($"api/Usuarios/{id}");
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var usuario = JsonConvert.DeserializeObject<Usuario>(content);
                return Convertir(usuario);
            }
            return null;
        }

        public async Task<UsuarioViewModel> UpdateUsuario(UsuarioViewModel usuarioViewModel)
        {
            var usuarioApi = Convertir(usuarioViewModel);
            var responseMessage = await _repository.PutResponse($"api/Usuarios/{usuarioViewModel.Id}", usuarioApi);
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                var updatedUsuario = JsonConvert.DeserializeObject<Usuario>(content);
                return Convertir(updatedUsuario);
            }
            return null;
        }

        private Usuario Convertir(UsuarioViewModel usuarioViewModel)
        {
            return new Usuario
            {
                Id = usuarioViewModel.Id,
                Nombre = usuarioViewModel.Nombre,
                Email = usuarioViewModel.Email,
                RolId = usuarioViewModel.RolId,
                DepartamentoId = usuarioViewModel.DepartamentoId ?? 0, // Uso de ?? porque DepartamentoId es int?
                NombreRol = usuarioViewModel.NombreRol,
                NombreDepartamento = usuarioViewModel.NombreDepartamento
            };
        }

        private UsuarioViewModel Convertir(Usuario usuario)
        {
            return new UsuarioViewModel
            {
                Id = usuario.Id,
                Nombre = usuario.Nombre,
                Email = usuario.Email,
                RolId = usuario.RolId,
                DepartamentoId = usuario.DepartamentoId,
                NombreRol = usuario.NombreRol,
                NombreDepartamento = usuario.NombreDepartamento
            };
        }
    }
}

