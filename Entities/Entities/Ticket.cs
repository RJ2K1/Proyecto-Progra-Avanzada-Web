using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Ticket
{
    public int Id { get; set; }

    public string NumeroTicket { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Descripcion { get; set; } = null!;

    public DateTime FechaDeCreacion { get; set; }

    public string Estado { get; set; } = null!;

    public DateTime? FechaActualizacion { get; set; }

    public string Complejidad { get; set; } = null!;

    public string Prioridad { get; set; } = null!;

    public int? Duracion { get; set; }

    public string Categoria { get; set; } = null!;

    public int? DepartamentoAsignadoId { get; set; }

    public int CreadoPorUsuarioId { get; set; }

    public int? AsignadoAusuarioId { get; set; }

    public virtual Usuarios? AsignadoAusuario { get; set; }

    public virtual Usuarios CreadoPorUsuario { get; set; } = null!;

    public virtual Departamentos? DepartamentoAsignado { get; set; }
}
