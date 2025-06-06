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
        public async Task<ActionResult<ApiResponseDTO<IEnumerable<ConocimientoResponseDTO>>>> ObtenerPorUsuario(int usuarioAdministradorId)
        {
            var response = await _conocimientoServicio.ObtenerConocimientosPorUsuarioAdministradorIdAsync(usuarioAdministradorId);
            if (!response.Exitoso)
            {
                return StatusCode(response.CodigoEstado, response);
            }
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponseDTO<ConocimientoResponseDTO>>> ObtenerPorId(int id)
        {
            var response = await _conocimientoServicio.ObtenerConocimientoPorIdAsync(id);
            if (!response.Exitoso)
            {
                return StatusCode(response.CodigoEstado, response);
            }
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponseDTO<IEnumerable<ConocimientoResponseDTO>>>> ObtenerTodos()
        {
            var response = await _conocimientoServicio.ObtenerTodosLosConocimientosAsync();
            if (!response.Exitoso)
            {
                return StatusCode(response.CodigoEstado, response);
            }
            return Ok(response);
        }
    }
}
