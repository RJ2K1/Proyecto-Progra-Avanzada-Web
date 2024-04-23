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
    public class TicketHelper : ITicketHelper
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _apiBaseUrl;
        private readonly string _apiKey;

        public TicketHelper(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _apiBaseUrl = configuration.GetValue<string>("BackEnd:Url");
            _apiKey = configuration.GetValue<string>("BackEnd:ApiKey");
        }

        private HttpClient CreateClient()
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("ApiKey", _apiKey);
            return client;
        }

        public async Task<List<TicketViewModel>> GetTickets()
        {
            var client = CreateClient();
            var response = await client.GetAsync($"{_apiBaseUrl}api/ticket");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<TicketViewModel>>(content);
            }
            return new List<TicketViewModel>();
        }

        public async Task<TicketViewModel> GetTicket(int id)
        {
            var client = CreateClient();
            var response = await client.GetAsync($"{_apiBaseUrl}api/ticket/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TicketViewModel>(content);
            }
            return null;
        }

        public async Task<TicketViewModel> AddTicket(TicketViewModel ticket)
        {
            var client = CreateClient();
            var json = JsonConvert.SerializeObject(ticket);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_apiBaseUrl}api/ticket", content);

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TicketViewModel>(responseData);
            }
            return null;
        }

        public async Task<bool> DeleteTicket(int id)
        {
            var client = CreateClient();
            var response = await client.DeleteAsync($"{_apiBaseUrl}api/ticket/{id}");

            return response.IsSuccessStatusCode;
        }

        public async Task<TicketViewModel> UpdateTicket(TicketViewModel ticket)
        {
            var client = CreateClient();
            var json = JsonConvert.SerializeObject(ticket);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{_apiBaseUrl}api/ticket/{ticket.Id}", content);

            if (response.IsSuccessStatusCode)
            {
                var contentResponse = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TicketViewModel>(contentResponse);
            }
            return null;
        }
    }
}
