using portafolio.backend.API.Dominio.DTOs.Conocimiento;
using portafolio.backend.API.Dominio.DTOs.Habilidad;

namespace portafolio.backend.API.Dominio.DTOs.Proyecto
{
    public class ProyectoResponseDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public string? ImagenDesktopUrl { get; set; }
        public string? ImagenMobileUrl { get; set; }
        public string? RepositorioUrl { get; set; }
        public string? LiveUrl { get; set; }
        public int Orden { get; set; }

        public List<HabilidadResponseDTO> Habilidades { get; set; } = new List<HabilidadResponseDTO>();
        public List<ConocimientoResponseDTO> Conocimientos { get; set; } = new List<ConocimientoResponseDTO>();
    }
}
