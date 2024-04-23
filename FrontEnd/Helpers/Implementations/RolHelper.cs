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
    public class RolHelper : IRolHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl;
        private readonly string _apiKey;
        private readonly ILogger<RolHelper> _logger;

        public RolHelper(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<RolHelper> logger)
        {
            _httpClientFactory = httpClientFactory;
            _apiBaseUrl = configuration.GetValue<string>("BackEnd:Url");
            _apiKey = configuration.GetValue<string>("BackEnd:ApiKey");
            _logger = logger;
        }

        private HttpClient CreateClient()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("ApiKey", _apiKey);
            return client;
        }

        public async Task<List<RolViewModel>> GetRoles()
        {
            var client = CreateClient();
            var response = await client.GetAsync($"{_apiBaseUrl}api/Roles");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<RolViewModel>>(content);
            }
            else
            {
                _logger.LogError($"Error al obtener roles. Código de estado: {response.StatusCode}");
                return new List<RolViewModel>();
            }
        }

        public async Task<RolViewModel> GetRol(int id)
        {
            var client = CreateClient();
            var response = await client.GetAsync($"{_apiBaseUrl}api/Roles/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<RolViewModel>(content);
            }
            else
            {
                _logger.LogError($"Error al obtener el rol con ID {id}. Código de estado: {response.StatusCode}");
                return null;
            }
        }

        public async Task AddRol(RolViewModel rolViewModel)
        {
            var client = CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(rolViewModel), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_apiBaseUrl}api/Roles", content);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Rol {rolViewModel.NombreRol} agregado con éxito.");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error al agregar rol: {errorContent}");
                throw new ApplicationException($"Error al agregar rol: {errorContent}");
            }
        }

        public async Task UpdateRol(RolViewModel rolViewModel)
        {
            var client = CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(rolViewModel), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{_apiBaseUrl}api/Roles/{rolViewModel.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Rol {rolViewModel.Id} actualizado con éxito.");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error al actualizar el rol {rolViewModel.Id}: {errorContent}");
                throw new ApplicationException($"Error al actualizar el rol: {errorContent}");
            }
        }

        public async Task DeleteRol(int id)
        {
            var client = CreateClient();
            var response = await client.DeleteAsync($"{_apiBaseUrl}api/Roles/{id}");

            if (response.IsSuccessStatusCode)
            {
                _logger.LogInformation($"Rol con ID {id} eliminado con éxito.");
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error al eliminar el rol con ID {id}: {errorContent}");
                throw new ApplicationException($"Error al eliminar el rol: {errorContent}");
            }
        }
    }
}
