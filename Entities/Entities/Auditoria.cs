using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Auditoria
{
    public int Id { get; set; }

    public string TablaAfectada { get; set; } = null!;

    public int RegistroId { get; set; }

    public string Accion { get; set; } = null!;

    public int UsuarioId { get; set; }

    public DateTime FechaAccion { get; set; }

    public virtual Usuarios Usuario { get; set; } = null!;
}
