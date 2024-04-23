using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FrontEnd.Helpers.Implementations
{
    public class AuditoriaHelper : IAuditoriaHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl;
        private readonly string _apiKey;
        private readonly ILogger<AuditoriaHelper> _logger;

        public AuditoriaHelper(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<AuditoriaHelper> logger)
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

        public async Task<List<Auditoria>> GetAuditorias()
        {
            var client = CreateClient();
            var response = await client.GetAsync($"{_apiBaseUrl}api/Auditoria");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var auditorias = JsonConvert.DeserializeObject<List<Auditoria>>(content);
                return auditorias;
            }
            else
            {
                _logger.LogError($"Error al obtener auditorías. Código de estado: {response.StatusCode}");
                return new List<Auditoria>();
            }
        }
    }
}
