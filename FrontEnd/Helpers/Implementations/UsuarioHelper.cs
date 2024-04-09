using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FrontEnd.Helpers.Implementations
{
    public class UsuarioHelper : IUsuarioHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl;
        private readonly ILogger<UsuarioHelper> _logger;

        public UsuarioHelper(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<UsuarioHelper> logger)
        {
            _httpClientFactory = httpClientFactory;
            _apiBaseUrl = configuration.GetValue<string>("BackEnd:Url");
            _logger = logger;
        }

        public async Task<UsuarioViewModel> AddUsuario(UsuarioViewModel usuarioViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(usuarioViewModel), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_apiBaseUrl}api/Usuarios", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var addedUsuario = JsonConvert.DeserializeObject<UsuarioViewModel>(responseContent);
                return addedUsuario;
            }
            else
            {
                _logger.LogError($"Error al agregar usuario. Respuesta: {response.StatusCode}");
                return null;
            }
        }

        public async Task<bool> DeleteUsuario(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_apiBaseUrl}api/Usuarios/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                _logger.LogError($"Error al eliminar usuario con ID {id}. Respuesta: {response.StatusCode}");
                return false;
            }
        }

        public async Task<List<UsuarioViewModel>> GetUsuarios()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_apiBaseUrl}api/usuarios");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var usuarios = JsonConvert.DeserializeObject<List<UsuarioViewModel>>(content);
                return usuarios;
            }
            else
            {
                _logger.LogError($"Error al obtener usuarios. Respuesta: {response.StatusCode}");
                return new List<UsuarioViewModel>();
            }
        }

        public async Task<UsuarioViewModel> GetUsuario(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_apiBaseUrl}api/Usuarios/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var usuario = JsonConvert.DeserializeObject<UsuarioViewModel>(content);
                return usuario;
            }
            else
            {
                _logger.LogError($"Error al obtener usuario con ID {id}. Respuesta: {response.StatusCode}");
                return null;
            }
        }

        public async Task<UsuarioViewModel> UpdateUsuario(UsuarioViewModel usuarioViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(usuarioViewModel), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{_apiBaseUrl}api/Usuarios/{usuarioViewModel.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var updatedUsuario = JsonConvert.DeserializeObject<UsuarioViewModel>(responseContent);
                return updatedUsuario;
            }
            else
            {
                _logger.LogError($"Error al actualizar usuario. Respuesta: {response.StatusCode}");
                return null;
            }
        }
    }
}
