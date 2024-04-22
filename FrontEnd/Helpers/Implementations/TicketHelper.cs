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
        private readonly ILogger<TicketHelper> _logger;

        public TicketHelper(IHttpClientFactory httpClientFactory, IConfiguration configuration, ILogger<TicketHelper> logger)
        {
            _httpClientFactory = httpClientFactory;
            _apiBaseUrl = configuration.GetValue<string>("BackEnd:Url");
            _logger = logger;
        }

        public async Task<List<TicketViewModel>> GetTickets()
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_apiBaseUrl}api/Tickets");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var tickets = JsonConvert.DeserializeObject<List<TicketViewModel>>(content);
                return tickets;
            }
            else
            {
                _logger.LogError($"Error al obtener tickets. Respuesta: {response.StatusCode}");
                return new List<TicketViewModel>();
            }
        }

        public async Task<TicketViewModel> GetTicket(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.GetAsync($"{_apiBaseUrl}api/Tickets/{id}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var ticket = JsonConvert.DeserializeObject<TicketViewModel>(content);
                return ticket;
            }
            else
            {
                _logger.LogError($"Error al obtener el ticket con ID {id}. Respuesta: {response.StatusCode}");
                return null;
            }
        }

        public async Task AddTicket(TicketViewModel ticketViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(ticketViewModel), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{_apiBaseUrl}api/Tickets", content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Error al agregar ticket. Respuesta: {response.StatusCode}");
            }
        }

        public async Task UpdateTicket(TicketViewModel ticketViewModel)
        {
            var client = _httpClientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(ticketViewModel), Encoding.UTF8, "application/json");
            var response = await client.PutAsync($"{_apiBaseUrl}api/Tickets/{ticketViewModel.Id}", content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Error al actualizar ticket. Respuesta: {response.StatusCode}");
            }
        }

        public async Task DeleteTicket(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var response = await client.DeleteAsync($"{_apiBaseUrl}api/Tickets/{id}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Error al eliminar ticket con ID {id}. Respuesta: {response.StatusCode}");
            }
        }
    }
}
