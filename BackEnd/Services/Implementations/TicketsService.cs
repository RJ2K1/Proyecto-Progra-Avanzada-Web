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

        public async Task<List<TicketModel>> GetTicketsByUserId(int userId)
        {
            var tickets = await _Unidad.TicketsDAL.FindAsync(t => t.CreadoPorUsuarioId == userId);
            return tickets.Select(Convertir).ToList();
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

        private Ticket Convertir(TicketModel ticketModel)
        {
            int userId = GetCurrentUserId();  // Method to fetch current user's ID from claims.

            return new Ticket
            {
                Id = ticketModel.Id,
                NumeroTicket = ticketModel.NumeroTicket,
                Nombre = ticketModel.Nombre,
                Descripcion = ticketModel.Descripcion,
                FechaDeCreacion = ticketModel.FechaDeCreacion == default(DateTime) ? DateTime.Now : ticketModel.FechaDeCreacion,
                Estado = ticketModel.Estado,
                FechaActualizacion = ticketModel.FechaActualizacion ?? DateTime.Now,  // Optionally set to now if null.
                Complejidad = ticketModel.Complejidad,
                Prioridad = ticketModel.Prioridad,
                Duracion = ticketModel.Duracion,
                Categoria = ticketModel.Categoria,
                DepartamentoAsignadoId = ticketModel.DepartamentoAsignadoId,
                CreadoPorUsuarioId = userId,  // Set from claims
                AsignadoAusuarioId = ticketModel.AsignadoAusuarioId
            };
        }

        private int GetCurrentUserId()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                string userIdValue = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return int.TryParse(userIdValue, out var userId) ? userId : 0;  // Return 0 or throw exception if needed
            }
            _logger.LogWarning("User is not authenticated.");
            return 0;  // Or handle this scenario as needed.
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
