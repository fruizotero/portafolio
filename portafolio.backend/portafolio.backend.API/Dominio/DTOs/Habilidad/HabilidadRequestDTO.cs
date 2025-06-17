using Microsoft.AspNetCore.Http;

namespace portafolio.backend.API.Dominio.DTOs.Habilidad
{
    public class HabilidadRequestDTO
    {
        public string Nombre { get; set; } = null!;
        public bool EsActual { get; set; } = true;
        public IFormFile? Logo { get; set; }
    }
}