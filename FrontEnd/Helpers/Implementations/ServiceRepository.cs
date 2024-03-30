using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FrontEnd.Helpers.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FrontEnd.Helpers.Implementations
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly HttpClient _client;

        public ServiceRepository(HttpClient client, IConfiguration configuration)
        {
            _client = client;
            _client.BaseAddress = new Uri(configuration.GetValue<string>("BackEnd:Url"));
        }

        public async Task<HttpResponseMessage> GetResponse(string url)
        {
            return await _client.GetAsync(url);
        }

        public async Task<HttpResponseMessage> PutResponse(string url, object model)
        {
            return await _client.PutAsJsonAsync(url, model);
        }

        public async Task<HttpResponseMessage> PostResponse(string url, object model)
        {
            return await _client.PostAsJsonAsync(url, model);
        }

        public async Task<HttpResponseMessage> DeleteResponse(string url)
        {
            return await _client.DeleteAsync(url);
        }
    }
}