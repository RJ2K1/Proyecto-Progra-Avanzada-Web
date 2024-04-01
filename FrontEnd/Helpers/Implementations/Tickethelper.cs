using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;

namespace FrontEnd.Helpers.Implementations
{
    public class Tickethelper : ITicketHelper
    {

        IServiceRepository ServiceRepository;

        public Tickethelper(IServiceRepository serviceRepository) {

            ServiceRepository = serviceRepository;


        }
        Tickets Convertir(TicketViewModel ticket) {

            return new Tickets
            {
                Id = ticket.Id,
                Nombre = ticket.Nombre,
                NumeroTicket = ticket.NumeroTicket,
                Estado = ticket.Estado,
                Categoria = ticket.Categoria,
                CreadoPorUsuarioId = ticket.CreadoPorUsuarioId,
                AsignadoAusuarioId = ticket.AsignadoAusuarioId,
                Descripcion = ticket.Descripcion,
                DepartamentoAsignadoId = ticket.DepartamentoAsignadoId,
                Complejidad = ticket.Complejidad,
                Duracion = ticket.Duracion,
                FechaActualizacion = ticket.FechaActualizacion,
                FechaDeCreacion = ticket.FechaDeCreacion,
                Prioridad = ticket.Prioridad
            };
        
        }
        TicketViewModel Convertir (Tickets ticket)
        {

            return new TicketViewModel
            {
                Id = ticket.Id,
                Nombre = ticket.Nombre,
                NumeroTicket = ticket.NumeroTicket,
                Estado = ticket.Estado,
                Categoria = ticket.Categoria,
                CreadoPorUsuarioId = ticket.CreadoPorUsuarioId,
                AsignadoAusuarioId = ticket.AsignadoAusuarioId,
                Descripcion = ticket.Descripcion,
                DepartamentoAsignadoId = ticket.DepartamentoAsignadoId,
                Complejidad = ticket.Complejidad,
                Duracion = ticket.Duracion,
                FechaActualizacion = ticket.FechaActualizacion,
                FechaDeCreacion = ticket.FechaDeCreacion,
                Prioridad = ticket.Prioridad

            };

        }


        public async Task<TicketViewModel> GetTicket(int id)
        {
            var responseMessage = await ServiceRepository.GetResponse($"api/Ticket/{id}");
            if (responseMessage.IsSuccessStatusCode) {
                var content=await responseMessage.Content.ReadAsStringAsync();
                var ticket = JsonConvert.DeserializeObject<Tickets>(content);
                return Convertir(ticket);
            }
            return null;
        }


        public async Task<TicketViewModel> AddTicket(TicketViewModel ticketViewModel)
        {
            var ticketApi = Convertir(ticketViewModel);
            var responseMessage = await ServiceRepository.PostResponse("api/Ticket", ticketApi);
            if (responseMessage.IsSuccessStatusCode) {
            
                var content= await responseMessage.Content.ReadAsStringAsync();
                var addeDTicket = JsonConvert.DeserializeObject<Tickets>(content);

                return Convertir(addeDTicket);
            }
            return null;
        }

        public async Task<bool> DeleteTicket(int id)
        {
            var responseMessage = await ServiceRepository.DeleteResponse($"api/Ticket/{id}");
            return responseMessage.IsSuccessStatusCode;
        }

        public async Task<TicketViewModel> UpdateTicket(TicketViewModel ticketViewModel)
        {
            var ticketApi = Convertir(ticketViewModel);
            var responseMessage = await ServiceRepository.PostResponse($"api/Ticket/{ticketViewModel.Id}", ticketApi);
            if (responseMessage.IsSuccessStatusCode)
            {

                var content = await responseMessage.Content.ReadAsStringAsync();
                var addeDTicket = JsonConvert.DeserializeObject<Tickets>(content);

                return Convertir(addeDTicket);
            }
            return null;
        }

        public async Task<List<TicketViewModel>> GetTickets()
        {
            var responseMessage = await ServiceRepository.GetResponse("api/Ticket");
            if (responseMessage.IsSuccessStatusCode)
            {
                var content= await responseMessage.Content.ReadAsStringAsync();
                var ticket = JsonConvert.DeserializeObject<List<Tickets>>(content);
                return ticket.ConvertAll(i => Convertir(i));
            }
            return new List<TicketViewModel>();
        }
    }
}
