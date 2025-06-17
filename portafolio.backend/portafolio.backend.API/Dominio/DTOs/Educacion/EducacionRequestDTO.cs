namespace portafolio.backend.API.Dominio.DTOs.Educacion
{
    public class EducacionRequestDTO
    {
        public string Institucion { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public string? Descripcion { get; set; }
    }
}