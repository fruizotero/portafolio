using Microsoft.AspNetCore.Http;

namespace portafolio.backend.API.Dominio.DTOs.RedSocialContacto
{
    public class RedSocialContactoRequestDTO
    {
        public string Plataforma { get; set; } = null!;
        public string Url { get; set; } = null!;
        public IFormFile? Icono { get; set; }
    }
}