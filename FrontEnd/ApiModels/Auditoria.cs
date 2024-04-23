namespace FrontEnd.ApiModels
{
    public class Auditoria
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
