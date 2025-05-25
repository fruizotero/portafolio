namespace portafolio.backend.API.Dominio.Entidades
{
    public class UsuarioAdministrador
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime FechaActualizacion { get; set; } = DateTime.UtcNow;
 
    }
}
