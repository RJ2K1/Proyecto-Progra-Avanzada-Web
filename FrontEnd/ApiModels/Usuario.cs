namespace FrontEnd.ApiModels
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Contrasena { get; set; } // Considera seguridad si expones contraseñas
        public int RolId { get; set; }
        public int DepartamentoId { get; set; }
        public string NombreRol { get; set; }
        public string NombreDepartamento { get; set; }
    }
}
