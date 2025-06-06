using System.ComponentModel.DataAnnotations;

namespace portafolio.backend.API.Dominio.DTOs.Imagen
{
    public class ImagenUploadRequest
    {
        private static readonly string[] AllowedContentTypes =
            new[] { "image/jpeg", "image/png", "image/gif" };

        private const long MaxFileSizeBytes = 10 * 1024 * 1024; // 5 MB

        public IFormFile Image { get; }

        public ImagenUploadRequest(IFormFile image)
        {
            Image = image ?? throw new ArgumentNullException(nameof(image), "No se proporcionó ningún archivo.");
            Validate();
        }

        private void Validate()
        {
            if (Image.Length == 0)
                throw new ArgumentException("El archivo de imagen está vacío.", nameof(Image));

            if (Image.Length > MaxFileSizeBytes)
                throw new ArgumentException(
                    $"La imagen excede el tamaño máximo permitido de {MaxFileSizeBytes / (1024 * 1024)} MB.",
                    nameof(Image));

            if (Array.IndexOf(AllowedContentTypes, Image.ContentType.ToLowerInvariant()) < 0)
                throw new ArgumentException(
                    $"Tipo de contenido no permitido: {Image.ContentType}. Solo se admiten: {string.Join(", ", AllowedContentTypes)}.",
                    nameof(Image));

            var extension = Path.GetExtension(Image.FileName).ToLowerInvariant();
            if (extension != ".jpg" && extension != ".jpeg" && extension != ".png" && extension != ".gif")
                throw new ArgumentException(
                    $"Extensión de archivo no permitida: {extension}. Debe ser .jpg, .jpeg, .png o .gif.",
                    nameof(Image));
        }
    }
}
