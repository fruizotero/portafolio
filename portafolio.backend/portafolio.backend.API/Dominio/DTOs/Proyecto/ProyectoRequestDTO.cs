using Microsoft.AspNetCore.Http;

namespace portafolio.backend.API.Dominio.DTOs.Proyecto
{
    public class ProyectoRequestDTO
    {
        public string Titulo { get; set; } = null!;
        public string Descripcion { get; set; } = null!;
        public IFormFile? ImagenDesktop { get; set; }
        public IFormFile? ImagenMobile { get; set; }
        public string? RepositorioUrl { get; set; }
        public string? LiveUrl { get; set; }
        public int Orden { get; set; } = 0;
        public List<int> HabilidadesIds { get; set; } = new List<int>();
        public List<int> ConocimientosIds { get; set; } = new List<int>();
    }
}