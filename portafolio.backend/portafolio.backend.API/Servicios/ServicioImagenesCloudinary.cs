using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using portafolio.backend.API.Dominio.DTOs.Imagen;
using portafolio.backend.API.Interfaces;

namespace portafolio.backend.API.Servicios
{
    public class ServicioImagenesCloudinary : ServicioImagenes
    {
        private readonly Cloudinary _cloudinary;

        public ServicioImagenesCloudinary(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }
        public async Task<ImagenUploadResponseDto> SubirImagenAsync(ImagenUploadRequest request)
        {
            using var stream = request.Image.OpenReadStream();

            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(request.Image.FileName, stream)
            };

            ImageUploadResult uploadResult;
            try
            {
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error al subir a Cloudinary: {ex.Message}", ex);
            }

            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new InvalidOperationException(
                    $"Cloudinary devolvió un error: {uploadResult.Error?.Message}"
                );
            }

            return new ImagenUploadResponseDto
            {
                PublicId = uploadResult.PublicId!,
                Url = uploadResult.SecureUrl.ToString()!
            };
        }
    }
}
