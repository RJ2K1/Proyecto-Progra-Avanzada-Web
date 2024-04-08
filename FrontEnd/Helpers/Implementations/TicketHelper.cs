using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd.Helpers.Implementations
{
    public class TicketHelper : ITicketHelper
    {
        private readonly IServiceRepository _repository;
        private const string BaseUrl = "api/ticket"; // Asegúrate de que esta URL es correcta para tu API

        public TicketHelper(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TicketViewModel>> GetTickets()
        {
            var response = await _repository.GetResponse(BaseUrl);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<TicketViewModel>>(content);
            }
            return new List<TicketViewModel>();
        }

        public async Task<TicketViewModel> GetTicket(int id)
        {
            var response = await _repository.GetResponse($"{BaseUrl}/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TicketViewModel>(content);
            }
            return null;
        }

        public async Task<TicketViewModel> AddTicket(TicketViewModel ticket)
        {
            var json = JsonConvert.SerializeObject(ticket);
            var response = await _repository.PostResponse(BaseUrl, new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TicketViewModel>(content);
            }
            return null;
        }

        public async Task<bool> DeleteTicket(int id)
        {
            var response = await _repository.DeleteResponse($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<TicketViewModel> UpdateTicket(TicketViewModel ticket)
        {
            var json = JsonConvert.SerializeObject(ticket);
            var response = await _repository.PutResponse($"{BaseUrl}/{ticket.Id}", new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TicketViewModel>(content);
            }
            return null;
        }
    }
}