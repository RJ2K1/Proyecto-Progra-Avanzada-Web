namespace FrontEnd.Models
{
    public class UsuarioCreateViewModel
    {
        public string Nombre { get; set; }
        public string Email { get; set; }
        public int RolId { get; set; }
        public int? DepartamentoId { get; set; }
        public string Contrasena { get; set; }
        public string ConfirmarContrasena { get; set; }
    }
}
