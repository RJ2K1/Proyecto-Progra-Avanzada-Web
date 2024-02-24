namespace BackEnd.Models
{
    public class UsuariosModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; } // Solo para creación/actualización
        public int RolId { get; set; }
        public int? DepartamentoId { get; set; }
        public string NombreRol { get; set; } // Solo para lectura
        public string NombreDepartamento { get; set; } // Solo para lectura
    }
}
