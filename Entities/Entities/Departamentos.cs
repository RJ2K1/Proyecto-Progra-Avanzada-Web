using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Departamentos
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public virtual ICollection<Ticket> Tickets { get; } = new List<Ticket>();

    public virtual ICollection<Usuarios> Usuarios { get; } = new List<Usuarios>();
}
