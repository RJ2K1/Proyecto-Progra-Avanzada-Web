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
    public class DepartamentoHelper : IDepartamentoHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl;
        private readonly string _apiKey;
        private readonly ILogger<DepartamentoHelper> _logger;

        public DepartamentoHelper(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<DepartamentoHelper> logger)
        {
            _httpClientFactory = httpClientFactory;
            _apiBaseUrl = configuration.GetValue<string>("BackEnd:Url");
            _apiKey = configuration.GetValue<string>("BackEnd:ApiKey");
            _logger = logger;
        }

        private HttpClient CreateClient()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("ApiKey", _apiKey); // Añadir la ApiKey a los headers
            return client;
        }

        public async Task<List<DepartamentoViewModel>> GetDepartamentos()
        {
            var client = CreateClient();
            var response = await client.GetAsync($"{_apiBaseUrl}api/Departamentos");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var departamentos = JsonConvert.DeserializeObject<List<DepartamentoViewModel>>(content);
                return departamentos;
            }
            else
            {
                _logger.LogError($"Error al obtener departamentos. Respuesta: {response.StatusCode}");
                return new List<DepartamentoViewModel>();
            }
        }

        public async Task<DepartamentoViewModel> GetDepartamento(int id)
        {
            var client = CreateClient();
            var response = await client.GetAsync($"{_apiBaseUrl}api/Departamentos/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var departamento = JsonConvert.DeserializeObject<DepartamentoViewModel>(content);
                return departamento;
            }
            else
            {
                _logger.LogError($"Error al obtener el departamento con ID {id}. Respuesta: {response.StatusCode}");
                return null;
            }
        }

        public async Task AddDepartamento(DepartamentoViewModel departamentoViewModel)
        {
            var client = CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(departamentoViewModel), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_apiBaseUrl}api/Departamentos", content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Error al agregar departamento. Respuesta: {response.StatusCode}");
            }
        }

        public async Task UpdateDepartamento(DepartamentoViewModel departamentoViewModel)
        {
            var client = CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(departamentoViewModel), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{_apiBaseUrl}api/Departamentos/{departamentoViewModel.Id}", content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Error al actualizar departamento. Respuesta: {response.StatusCode}");
            }
        }

        public async Task DeleteDepartamento(int id)
        {
            var client = CreateClient();
            var response = await client.DeleteAsync($"{_apiBaseUrl}api/Departamentos/{id}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Error al eliminar departamento con ID {id}. Respuesta: {response.StatusCode}");
            }
        }
    }
}
