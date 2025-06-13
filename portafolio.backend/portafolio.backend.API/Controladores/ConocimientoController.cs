using Microsoft.AspNetCore.Mvc;
using portafolio.backend.API.Dominio.DTOs;
using portafolio.backend.API.Dominio.DTOs.Conocimiento;
using portafolio.backend.API.Servicios;

namespace portafolio.backend.API.Controladores
{
    [ApiController]
    [Route("[controller]")]
    public class ConocimientoController : ControllerBase
    {
        private readonly ConocimientoServicio _conocimientoServicio;

        public ConocimientoController(ConocimientoServicio conocimientoServicio)
        {
            _conocimientoServicio = conocimientoServicio ?? throw new ArgumentNullException(nameof(conocimientoServicio));
        }

        [HttpGet("usuario/{usuarioAdministradorId}")]
        public async Task<ActionResult<ApiResponseDTO<IEnumerable<ConocimientoResponseDTO>>>> ObtenerConocimientosPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            var response = await _conocimientoServicio.ObtenerConocimientosPorUsuarioAdministradorIdAsync(usuarioAdministradorId);
            return StatusCode(response.CodigoEstado, response);
        }

        [HttpPost("{usuarioAdministradorId}")]
        public async Task<ActionResult<ApiResponseDTO<ConocimientoResponseDTO>>> CrearConocimiento(int usuarioAdministradorId, [FromBody] ConocimientoRequestDTO conocimientoRequest)
        {
            var response = await _conocimientoServicio.CrearConocimientoAsync(usuarioAdministradorId, conocimientoRequest);
            return StatusCode(response.CodigoEstado, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseDTO<ConocimientoResponseDTO>>> ObtenerPorId(int id)
        {
            var response = await _conocimientoServicio.ObtenerConocimientoPorIdAsync(id);
            return StatusCode(response.CodigoEstado, response);
        }

      
    }
}
