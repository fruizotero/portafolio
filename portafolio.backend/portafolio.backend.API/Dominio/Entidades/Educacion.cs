// Dominio/Entidades/Educacion.cs
namespace portafolio.backend.API.Dominio.Entidades
{
    public class Educacion
    {
        public int Id { get; set; }
        
        public UsuarioAdministrador UsuarioAdministrador { get; set; } = null!;
        public int UsuarioAdministradorId { get; set; }
        
        public string Institucion { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Descripcion { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
