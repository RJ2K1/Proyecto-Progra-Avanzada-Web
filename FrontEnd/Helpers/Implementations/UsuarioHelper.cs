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
        private readonly string _apiKey;
        private readonly ILogger<UsuarioHelper> _logger;

        public UsuarioHelper(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<UsuarioHelper> logger)
        {
            _httpClientFactory = httpClientFactory;
            _apiBaseUrl = configuration.GetValue<string>("BackEnd:Url");
            _apiKey = configuration.GetValue<string>("BackEnd:ApiKey"); // Obtener la ApiKey del appsettings.json
            _logger = logger;
        }

        private HttpClient CreateClient()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("ApiKey", _apiKey); // Añadir la ApiKey a los headers
            return client;
        }

        public async Task<bool> AddUsuario(UsuarioCreateViewModel usuarioCreateViewModel)
        {
            var client = CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(usuarioCreateViewModel), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_apiBaseUrl}api/Usuarios", content);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation("Usuario creado exitosamente en el backend.");
                return true;
            }
            else
            {
                _logger.LogError($"Error al agregar usuario. Respuesta: {response.StatusCode}");
                return false;
            }
        }

        public async Task<bool> DeleteUsuario(int id)
        {
            var client = CreateClient();
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
            var client = CreateClient();
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
            var client = CreateClient();
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

        public async Task<bool> UpdateUsuario(UsuarioUpdateViewModel usuarioUpdateViewModel)
        {
            var client = CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(usuarioUpdateViewModel), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{_apiBaseUrl}api/Usuarios/{usuarioUpdateViewModel.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                _logger.LogError($"Error al actualizar usuario. ID: {usuarioUpdateViewModel.Id}, Respuesta: {response.StatusCode}");
                return false;
            }
        }
    }
}
