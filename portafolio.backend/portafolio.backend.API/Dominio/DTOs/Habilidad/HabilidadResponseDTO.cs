namespace portafolio.backend.API.Dominio.DTOs.Habilidad
{
    public class HabilidadResponseDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? LogoUrl { get; set; }
        public bool EsActual { get; set; }
    }
}
