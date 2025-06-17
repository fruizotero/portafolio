using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace portafolio.backend.API.Dominio.Entidades
{
    public class Proyecto
    {

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string? ImagenDesktopUrl { get; set; }
        public string? ImagenMobileUrl { get; set; }
        public string? RepositorioUrl { get; set; }
        public string? LiveUrl { get; set; }
        public int Orden { get; set; } = 0;
        public   UsuarioAdministrador UsuarioAdministrador { get; set; }
        public int UsuarioAdministradorId { get; set; }

        public ICollection<Habilidad> Habilidades { get; set; } = new List<Habilidad>();
        public ICollection<Conocimiento> Conocimientos { get; set; } = new List<Conocimiento>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    }
}
