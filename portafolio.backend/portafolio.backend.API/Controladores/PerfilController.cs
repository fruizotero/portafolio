using Microsoft.AspNetCore.Mvc;
using portafolio.backend.API.Dominio.DTOs;
using portafolio.backend.API.Dominio.DTOs.Perfil;

namespace portafolio.backend.API.Controladores
{
    [ApiController]
    [Route("[controller]")]
    public class PerfilController : ControllerBase
    {
       private readonly Servicios.PerfilServicio _perfilServicio;
        public PerfilController(Servicios.PerfilServicio perfilServicio)
        {
            _perfilServicio = perfilServicio ?? throw new ArgumentNullException(nameof(perfilServicio));
        }

        [HttpGet("{usuarioAdministradorId}")]
        public async Task<IActionResult> ObtenerPerfilPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            var response = await _perfilServicio.ObtenerPerfilPorUsuarioAdministradorIdAsync(usuarioAdministradorId);
            if (!response.Exitoso)
            {
                return StatusCode(response.CodigoEstado, response);
            }
            return Ok(response);
        }

        [HttpPost("{usuarioAdministradorId}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ApiResponseDTO<string>>> CrearOActualizarPerfil(int usuarioAdministradorId, [FromForm] PerfilRequestDTO perfilRequest)
        {
            var response = await _perfilServicio.CrearOActualizarPerfilAsync(usuarioAdministradorId, perfilRequest);
            return StatusCode(response.CodigoEstado, response);
        }
    }
}
