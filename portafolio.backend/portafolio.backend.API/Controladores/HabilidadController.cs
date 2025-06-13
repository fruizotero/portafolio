using Microsoft.AspNetCore.Mvc;
using portafolio.backend.API.Dominio.DTOs;
using portafolio.backend.API.Dominio.DTOs.Habilidad;
using portafolio.backend.API.Servicios;

namespace portafolio.backend.API.Controladores
{
    [ApiController]
    [Route("[controller]")]
    public class HabilidadController : ControllerBase
    {
        private readonly HabilidadServicio _habilidadServicio;

        public HabilidadController(HabilidadServicio habilidadServicio)
        {
            _habilidadServicio = habilidadServicio ?? throw new ArgumentNullException(nameof(habilidadServicio));
        }

        [HttpGet("usuario/{usuarioAdministradorId}")]
        public async Task<ActionResult<ApiResponseDTO<IEnumerable<HabilidadResponseDTO>>>> ObtenerHabilidadesPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            var response = await _habilidadServicio.ObtenerHabilidadesPorUsuarioAdministradorIdAsync(usuarioAdministradorId);
            return StatusCode(response.CodigoEstado, response);
        }

        [HttpGet("usuario/{usuarioAdministradorId}/actuales")]
        public async Task<ActionResult<ApiResponseDTO<IEnumerable<HabilidadResponseDTO>>>> ObtenerHabilidadesActualesPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            var response = await _habilidadServicio.ObtenerHabilidadesActualesPorUsuarioAdministradorIdAsync(usuarioAdministradorId);
            return StatusCode(response.CodigoEstado, response);
        }

        [HttpPost("{usuarioAdministradorId}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ApiResponseDTO<HabilidadResponseDTO>>> CrearHabilidad(int usuarioAdministradorId, [FromForm] HabilidadRequestDTO habilidadRequest)
        {
            var response = await _habilidadServicio.CrearHabilidadAsync(usuarioAdministradorId, habilidadRequest);
            return StatusCode(response.CodigoEstado, response);
        }

        [HttpDelete("{id}/usuario/{usuarioAdministradorId}")]
        public async Task<ActionResult<ApiResponseDTO<string>>> EliminarHabilidad(int id, int usuarioAdministradorId)
        {
            var response = await _habilidadServicio.EliminarHabilidadAsync(id, usuarioAdministradorId);
            return StatusCode(response.CodigoEstado, response);
        }
    }
}
