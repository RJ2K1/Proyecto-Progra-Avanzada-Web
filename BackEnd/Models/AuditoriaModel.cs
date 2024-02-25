// AuditoriaModel.cs
using System;

namespace BackEnd.Models
{
    public class AuditoriaModel
    {
        public int Id { get; set; }
        public string TablaAfectada { get; set; }
        public int RegistroId { get; set; }
        public string Accion { get; set; }
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public DateTime FechaAccion { get; set; }
    }
}
