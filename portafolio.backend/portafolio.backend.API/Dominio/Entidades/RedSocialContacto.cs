// Dominio/Entidades/RedSocialContacto.cs
namespace portafolio.backend.API.Dominio.Entidades
{
    public class RedSocialContacto
    {
        public int Id { get; set; }
        
        public UsuarioAdministrador UsuarioAdministrador { get; set; } = null!;
        public int UsuarioAdministradorId { get; set; }
        
        public string Plataforma { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string? IconUrl { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
