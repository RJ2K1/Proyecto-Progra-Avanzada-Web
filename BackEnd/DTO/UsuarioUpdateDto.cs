namespace BackEnd.DTO
{
    public class UsuarioUpdateDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public int RolId { get; set; }
        public int? DepartamentoId { get; set; }
    }

}
