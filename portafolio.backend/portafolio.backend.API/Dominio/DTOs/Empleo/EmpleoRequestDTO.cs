using System.ComponentModel.DataAnnotations;

namespace portafolio.backend.API.Dominio.DTOs.Empleo
{
    public class EmpleoRequestDTO
    {
        public string Empresa { get; set; } = null!;
        public string Cargo { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Descripcion { get; set; }
    }
}