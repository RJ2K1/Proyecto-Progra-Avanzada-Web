using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Usuarios
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Contrasena { get; set; } = null!;

    public int RolId { get; set; }

    public int? DepartamentoId { get; set; }

    public virtual ICollection<Auditoria> Auditoria { get; } = new List<Auditoria>();

    public virtual Departamentos? Departamento { get; set; }

    public virtual Roles Rol { get; set; } = null!;

    public virtual ICollection<Ticket> TicketAsignadoAusuarios { get; } = new List<Ticket>();

    public virtual ICollection<Ticket> TicketCreadoPorUsuarios { get; } = new List<Ticket>();
}
