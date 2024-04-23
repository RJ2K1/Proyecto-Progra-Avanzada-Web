using BackEnd.Models;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace BackEnd.Services.Implementations
{
    public class TicketsService : ITicketsService
    {
        private readonly IUnidadDeTrabajo _Unidad;
        private readonly IAuditoriaService _auditoriaService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<TicketsService> _logger;

        public TicketsService(IUnidadDeTrabajo unidadTrabajo, IAuditoriaService auditoriaService, IHttpContextAccessor httpContextAccessor, ILogger<TicketsService> logger)
        {
            _Unidad = unidadTrabajo;
            _auditoriaService = auditoriaService;
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        public async Task<bool> add(TicketModel ticketModel)
        {
            var ticket = Convertir(ticketModel);
            await _Unidad.TicketsDAL.AddAsync(ticket);
            var result = await _Unidad.CompleteAsync();
            if (result)
            {
                RegistrarAuditoria("Creación de Ticket", ticket.Id);
            }
            return result;
        }

        public async Task<bool> delete(int id)
        {
            var ticket = await _Unidad.TicketsDAL.GetAsync(id);
            if (ticket == null) return false;

            await _Unidad.TicketsDAL.RemoveAsync(ticket);
            var result = await _Unidad.CompleteAsync();
            if (result)
            {
                RegistrarAuditoria("Eliminación de Ticket", id);
            }
            return result;
        }


        public async Task<TicketModel> getById(int id)
        {
            var ticket = await _Unidad.TicketsDAL.GetAsync(id);
            return Convertir(ticket);
        }

        public async Task<List<TicketModel>> GetTickts()
        {
            var ticket= await _Unidad.TicketsDAL.GetAllAsync();
            var List= ticket.Select(Convertir).ToList();
            return List;
        }

        public async Task<bool> Update(TicketModel ticketModel)
        {
            var ticket = await _Unidad.TicketsDAL.GetAsync(ticketModel.Id);
            if (ticket == null) return false;

            ticket = Convertir(ticketModel);
            await _Unidad.TicketsDAL.UpdateAsync(ticket);
            var result = await _Unidad.CompleteAsync();
            if (result)
            {
                RegistrarAuditoria("Actualización de Ticket", ticketModel.Id);
            }
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

        private async void RegistrarAuditoria(string accion, int registroId)
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                string userIdValue = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                int userId = int.TryParse(userIdValue, out var uid) ? uid : 0; // Handle default or error case as needed

                await _auditoriaService.Add(new AuditoriaModel
                {
                    Accion = accion,
                    FechaAccion = DateTime.UtcNow,
                    RegistroId = registroId,
                    TablaAfectada = "Tickets",
                    UsuarioId = userId
                });
            }
            else
            {
                _logger.LogWarning("Intento de registrar auditoría sin usuario autenticado.");
            }
        }
    }
}
