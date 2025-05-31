namespace portafolio.backend.API.Dominio.Entidades
{
    public class Habilidad
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? LogoUrl { get; set; }
        public bool EsActual { get; set; }
        public int UsuarioAdministradorId { get; set; }
        public UsuarioAdministrador UsuarioAdministrador { get; set; } = null!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
