// ServiceRepository.cs
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FrontEnd.Helpers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FrontEnd.Helpers.Implementations
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly HttpClient _client;
        private readonly ILogger<ServiceRepository> _logger;

        public ServiceRepository(HttpClient client, IConfiguration configuration, ILogger<ServiceRepository> logger)
        {
            _client = client;
            string baseUrl = configuration.GetValue<string>("BackEnd:Url");
            _client.BaseAddress = new Uri(baseUrl);
            _logger = logger;
        }

        public async Task<HttpResponseMessage> GetResponse(string url)
        {
            try
            {
                var response = await _client.GetAsync(url);
                _logger.LogInformation($"GET Request to {url} returned {response.StatusCode}");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GET Request to {url} failed", url);
                throw;
            }
        }

        public async Task<HttpResponseMessage> PutResponse(string url, object model)
        {
            try
            {
                var response = await _client.PutAsJsonAsync(url, model);
                _logger.LogInformation($"PUT Request to {url} with body returned {response.StatusCode}");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "PUT Request to {url} failed", url);
                throw;
            }
        }

        public async Task<HttpResponseMessage> PostResponse(string url, object model)
        {
            try
            {
                var response = await _client.PostAsJsonAsync(url, model);
                _logger.LogInformation($"POST Request to {url} with body returned {response.StatusCode}");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "POST Request to {url} failed", url);
                throw;
            }
        }

        public async Task<HttpResponseMessage> DeleteResponse(string url)
        {
            try
            {
                var response = await _client.DeleteAsync(url);
                _logger.LogInformation($"DELETE Request to {url} returned {response.StatusCode}");
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DELETE Request to {url} failed", url);
                throw;
            }
        }
    }
}
