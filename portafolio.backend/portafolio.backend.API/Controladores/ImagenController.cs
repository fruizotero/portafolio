using Microsoft.AspNetCore.Mvc;
using portafolio.backend.API.Dominio.DTOs.Imagen;
using portafolio.backend.API.Interfaces;
using static System.Net.Mime.MediaTypeNames;

namespace portafolio.backend.API.Controladores
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImagenController : ControllerBase
    {
        private readonly ServicioImagenes _servicioImagen;

        public ImagenController(ServicioImagenes servicioImagen)
        {
            _servicioImagen = servicioImagen ?? throw new ArgumentNullException(nameof(servicioImagen));
        }

        [HttpPost("Upload")]
        [RequestSizeLimit(10_000_000)] // Límite de 10 MB (ajústalo si lo necesitas)
        public async Task<IActionResult> Upload([FromForm] ImagenUploadDto imagenUploadDto)
        {
            // Crear y validar el VO
            ImagenUploadRequest requestDto;
            try
            {
                requestDto = new ImagenUploadRequest(imagenUploadDto.Image);
            }
            catch (ArgumentNullException exNull)
            {
                return BadRequest(new { error = exNull.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }

            // Llamar al servicio
            ImagenUploadResponseDto responseDto;
            try
            {
                responseDto = await _servicioImagen.SubirImagenAsync(requestDto);
            }
            catch (InvalidOperationException exSrv)
            {
                return StatusCode(500, new { error = "Error al subir la imagen.", details = exSrv.Message });
            }

            return Ok(responseDto);
        }
    }
}
