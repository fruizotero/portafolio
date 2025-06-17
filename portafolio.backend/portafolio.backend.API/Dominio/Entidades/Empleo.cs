// Dominio/Entidades/Empleo.cs
namespace portafolio.backend.API.Dominio.Entidades
{
    public class Empleo
    {
        public int Id { get; set; }
        
        public UsuarioAdministrador UsuarioAdministrador { get; set; } = null!;
        public int UsuarioAdministradorId { get; set; }
        
        public string Empresa { get; set; } = string.Empty;
        public string Cargo { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Descripcion { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
