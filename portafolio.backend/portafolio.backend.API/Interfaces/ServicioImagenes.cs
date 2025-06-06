using portafolio.backend.API.Dominio.DTOs.Imagen;

namespace portafolio.backend.API.Interfaces
{
    public interface ServicioImagenes
    {

        //subir imagen
        Task<ImagenUploadResponseDto> SubirImagenAsync(ImagenUploadRequest imagenUploadRequest);

    }
}
