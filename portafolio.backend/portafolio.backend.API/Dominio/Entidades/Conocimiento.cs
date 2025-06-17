namespace portafolio.backend.API.Dominio.Entidades
{
    public class Conocimiento
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public UsuarioAdministrador UsuarioAdministrador { get; set; }
        public int UsuarioAdministradorId { get; set; }

        public ICollection<Proyecto> Proyectos { get; set; } = new List<Proyecto>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
