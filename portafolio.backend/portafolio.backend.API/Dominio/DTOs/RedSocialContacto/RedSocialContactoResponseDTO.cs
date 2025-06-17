// Dominio/DTOs/RedSocialContacto/RedSocialContactoResponseDTO.cs
namespace portafolio.backend.API.Dominio.DTOs.RedSocialContacto
{
    public class RedSocialContactoResponseDTO
    {
        public int Id { get; set; }
        public string Plataforma { get; set; } = null!;
        public string Url { get; set; } = null!;
        public string? IconUrl { get; set; }
    }
}
