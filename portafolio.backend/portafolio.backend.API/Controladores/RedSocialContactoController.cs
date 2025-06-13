// Controladores/RedSocialContactoController.cs
using Microsoft.AspNetCore.Mvc;
using portafolio.backend.API.Dominio.DTOs;
using portafolio.backend.API.Dominio.DTOs.RedSocialContacto;
using portafolio.backend.API.Servicios;

namespace portafolio.backend.API.Controladores
{
    [ApiController]
    [Route("[controller]")]
    public class RedSocialContactoController : ControllerBase
    {
        private readonly RedSocialContactoServicio _redSocialServicio;

        public RedSocialContactoController(RedSocialContactoServicio redSocialServicio)
        {
            _redSocialServicio = redSocialServicio ?? throw new ArgumentNullException(nameof(redSocialServicio));
        }

        [HttpGet("{usuarioAdministradorId}")]
        public async Task<ActionResult<ApiResponseDTO<IEnumerable<RedSocialContactoResponseDTO>>>> ObtenerRedesSocialesPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            var response = await _redSocialServicio.ObtenerRedesSocialesPorUsuarioAdministradorIdAsync(usuarioAdministradorId);
            return StatusCode(response.CodigoEstado, response);
        }

        [HttpPost("{usuarioAdministradorId}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ApiResponseDTO<RedSocialContactoResponseDTO>>> CrearRedSocialContacto(int usuarioAdministradorId, [FromForm] RedSocialContactoRequestDTO redSocialRequest)
        {
            var response = await _redSocialServicio.CrearRedSocialContactoAsync(usuarioAdministradorId, redSocialRequest);
            return StatusCode(response.CodigoEstado, response);
        }
    }
}
