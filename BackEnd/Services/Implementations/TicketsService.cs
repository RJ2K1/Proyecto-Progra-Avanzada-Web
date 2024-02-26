using BackEnd.Models;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using Microsoft.Identity.Client;
using System.Runtime.InteropServices;

namespace BackEnd.Services.Implementations
{
    public class TicketsService : ITicketsService
    {
        private IUnidadDeTrabajo _Unidad;

        public TicketsService(IUnidadDeTrabajo unidadTrabajo)
        {
            _Unidad = unidadTrabajo;
        }

        public async Task<bool> add(TicketModel ticketModel)
        {
           var ticket = Convertir(ticketModel);
            await _Unidad._ticketsDAL.AddAsync(ticket);
            var result = _Unidad.Complete();
            return result;
        }

        public async Task<bool> delete(int id)
        {
            var ticket = new Ticket { Id = id };
            await _Unidad._ticketsDAL.RemoveAsync(ticket);
            var result= _Unidad.Complete();
            return result;
        }

        public async Task<TicketModel> getById(int id)
        {
            var ticket = await _Unidad._ticketsDAL.GetAsync(id);
            return Convertir(ticket);
        }

        public async Task<List<TicketModel>> GetTickts()
        {
            var ticket= await _Unidad._ticketsDAL.GetAllAsync();
            var List= ticket.Select(Convertir).ToList();
            return List;
        }

        public async Task<bool> Update(TicketModel ticketModel)
        {
            var ticket = Convertir(ticketModel);
            await _Unidad._ticketsDAL.UpdateAsync(ticket);
            var result= _Unidad.Complete(); 
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

        private Ticket Convertir(TicketModel ticketmodel) {


            return new Ticket
            {
                Id = ticketmodel.Id,
                NumeroTicket=ticketmodel.NumeroTicket,
                Nombre=ticketmodel.Nombre,
                Descripcion=ticketmodel.Descripcion,
                FechaDeCreacion=ticketmodel.FechaDeCreacion,
                Estado=ticketmodel.Estado,
                FechaActualizacion=ticketmodel.FechaActualizacion,
                Complejidad=ticketmodel.Complejidad,
                Prioridad=ticketmodel.Prioridad,
                Duracion=ticketmodel.Duracion,
                Categoria=ticketmodel.Categoria,
                DepartamentoAsignadoId=ticketmodel.DepartamentoAsignadoId,
                CreadoPorUsuarioId=ticketmodel.CreadoPorUsuarioId,
                AsignadoAusuarioId=ticketmodel.AsignadoAusuarioId
            };
        }
    }
}
