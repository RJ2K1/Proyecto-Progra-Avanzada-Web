using BackEnd.Models;
using BackEnd.Services.Interfaces;
using DAL.Implementations;
using DAL.Interfaces;
using Entities.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services.Implementations
{
    public class TicketsService : ITicketsService
    {
        private readonly IUnidadDeTrabajo _unidadDeTrabajo;

        public TicketsService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        public async Task<bool> Add(TicketModel ticketModel)
        {
            var ticket = new Ticket();
            Convertir(ticketModel, ticket);
            await _unidadDeTrabajo.TicketsDAL.AddAsync(ticket);
            var result = await _unidadDeTrabajo.CompleteAsync();
            return result;
        }

        public async Task<bool> Delete(int id)
        {
            var ticket = new Ticket { Id = id };
            await _unidadDeTrabajo.TicketsDAL.RemoveAsync(ticket);
            var result = await _unidadDeTrabajo.CompleteAsync();
            return result;
        }

        public async Task<TicketModel> GetById(int id)
        {
            var ticket = await _unidadDeTrabajo.TicketsDAL.GetAsync(id);
            return Convertir(ticket);
        }

        public async Task<List<TicketModel>> GetTickets()
        {
            var ticket = await _unidadDeTrabajo.TicketsDAL.GetAllAsync();
            var List = ticket.Select(Convertir).ToList();
            return List;
        }

        public async Task<bool> Update(TicketModel ticketModel)
        {
            var ticket = await _unidadDeTrabajo.TicketsDAL.GetAsync(ticketModel.Id);
            if (ticket == null)
            {
                return false;
            }
            ticket.NumeroTicket = ticketModel.NumeroTicket;
            ticket.Nombre = ticketModel.Nombre;
            ticket.Descripcion = ticketModel.Descripcion;
            ticket.FechaDeCreacion = ticketModel.FechaDeCreacion;
            ticket.Estado = ticketModel.Estado;
            ticket.FechaActualizacion = ticketModel.FechaActualizacion;
            ticket.Complejidad = ticketModel.Complejidad;
            ticket.Prioridad = ticketModel.Prioridad;
            ticket.Duracion = ticketModel.Duracion;
            ticket.Categoria = ticketModel.Categoria;
            ticket.DepartamentoAsignadoId = ticketModel.DepartamentoAsignadoId;
            ticket.CreadoPorUsuarioId = ticketModel.CreadoPorUsuarioId;
            ticket.AsignadoAusuarioId = ticketModel.AsignadoAusuarioId;
            ticket = Convertir(ticketModel, ticket);
            await _unidadDeTrabajo.TicketsDAL.UpdateAsync(ticket);
            var result = await _unidadDeTrabajo.CompleteAsync();
            return result;
        }

        private TicketModel Convertir(Ticket ticket)
        {
            return new TicketModel
            {

                Id = ticket.Id,
                NumeroTicket = ticket.NumeroTicket,
                Nombre = ticket.Nombre,
                Descripcion = ticket.Descripcion,
                FechaDeCreacion = ticket.FechaDeCreacion,
                Estado = ticket.Estado,
                FechaActualizacion = ticket.FechaActualizacion,
                Complejidad = ticket.Complejidad,
                Prioridad = ticket.Prioridad,
                Duracion = ticket.Duracion,
                Categoria = ticket.Categoria,
                DepartamentoAsignadoId = ticket.DepartamentoAsignadoId,
                CreadoPorUsuarioId = ticket.CreadoPorUsuarioId,
                AsignadoAusuarioId = ticket.AsignadoAusuarioId

            };
        }

        private Ticket Convertir(TicketModel ticketModel, Ticket ticket)
        {
            ticket.Id = ticketModel.Id;
            ticket.NumeroTicket = ticketModel.NumeroTicket;
            ticket.Nombre = ticketModel.Nombre;
            ticket.Descripcion = ticketModel.Descripcion;
            ticket.FechaDeCreacion = ticketModel.FechaDeCreacion;
            ticket.Estado = ticketModel.Estado;
            ticket.FechaActualizacion = ticketModel.FechaActualizacion;
            ticket.Complejidad = ticketModel.Complejidad;
            ticket.Prioridad = ticketModel.Prioridad;
            ticket.Duracion = ticketModel.Duracion;
            ticket.Categoria = ticketModel.Categoria;
            ticket.DepartamentoAsignadoId = ticketModel.DepartamentoAsignadoId;
            ticket.CreadoPorUsuarioId = ticketModel.CreadoPorUsuarioId;
            ticket.AsignadoAusuarioId = ticketModel.AsignadoAusuarioId;
            return ticket;
        }

    }
}
