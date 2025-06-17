using System.ComponentModel.DataAnnotations;

namespace portafolio.backend.API.Dominio.DTOs.Imagen
{
    public class ImagenUploadDto
    {
        [Required(ErrorMessage = "Debe proporcionar un archivo de imagen.")]
        public IFormFile Image { get; set; } = null!;
    }
}
