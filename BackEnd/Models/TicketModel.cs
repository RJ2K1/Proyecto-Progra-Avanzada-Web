using Entities.Entities;

namespace BackEnd.Models
{
    public class TicketModel
    {
        public int Id { get; set; }

        public required string NumeroTicket { get; set; }

        public required string Nombre { get; set; }

        public required string Descripcion { get; set; }

        public DateTime FechaDeCreacion { get; set; }

        public required string Estado { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public required string Complejidad { get; set; }

        public required string Prioridad { get; set; }

        public int? Duracion { get; set; }

        public required string Categoria { get; set; }

        public int? DepartamentoAsignadoId { get; set; }

        public int CreadoPorUsuarioId { get; set; }

        public int? AsignadoAusuarioId { get; set; }

    }
}
