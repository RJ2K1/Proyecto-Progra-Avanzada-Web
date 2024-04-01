namespace FrontEnd.Models
{
    public class TicketViewModel
    {
        public int Id { get; set; }

        public string NumeroTicket { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaDeCreacion { get; set; }

        public string Estado { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        public string Complejidad { get; set; }

        public string Prioridad { get; set; }

        public int? Duracion { get; set; }

        public string Categoria { get; set; }

        public int? DepartamentoAsignadoId { get; set; }

        public int CreadoPorUsuarioId { get; set; }

        public int? AsignadoAusuarioId { get; set; }
    }
}
