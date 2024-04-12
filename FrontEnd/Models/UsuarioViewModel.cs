namespace FrontEnd.Models
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public int RolId { get; set; }
        public int? DepartamentoId { get; set; }
        public string NombreRol { get; set; } // Solo para lectura
        public string NombreDepartamento { get; set; } // Solo para lectura
        public string Contrasena { get; set; } // Añadir para creación y puede ser null para actualización
    }
}
