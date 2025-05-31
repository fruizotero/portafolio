using Microsoft.AspNetCore.Mvc;
using portafolio.backend.API.Dominio.DTOs;
using portafolio.backend.API.Dominio.DTOs.Proyecto;
using portafolio.backend.API.Servicios;

namespace portafolio.backend.API.Controladores
{
    [ApiController]
    [Route("[controller]")]
    public class ProyectoController : ControllerBase
    {
        private readonly ProyectoServicio _proyectoServicio;
        
        public ProyectoController(ProyectoServicio proyectoServicio)
        {
            _proyectoServicio = proyectoServicio ?? throw new ArgumentNullException(nameof(proyectoServicio));
        }

        [HttpGet("usuario/{usuarioAdministradorId}")]
        public async Task<ActionResult<ApiResponseDTO<IEnumerable<ProyectoResponseDTO>>>> ObtenerPorUsuario(int usuarioAdministradorId)
        {
            var response = await _proyectoServicio.ObtenerPorUsuarioAsync(usuarioAdministradorId);
            if (!response.Exitoso)
            {
                return StatusCode(response.CodigoEstado, response);
            }
            return Ok(response);
        }

        [HttpGet("{id}/usuario/{usuarioAdministradorId}")]
        public async Task<ActionResult<ApiResponseDTO<ProyectoResponseDTO>>> ObtenerPorId(int id, int usuarioAdministradorId)
        {
            var response = await _proyectoServicio.ObtenerPorIdAsync(id, usuarioAdministradorId);
            if (!response.Exitoso)
            {
                return StatusCode(response.CodigoEstado, response);
            }
            return Ok(response);
        }
        
        [HttpGet("destacados/usuario/{usuarioAdministradorId}")]
        public async Task<ActionResult<ApiResponseDTO<IEnumerable<ProyectoResponseDTO>>>> ObtenerDestacados(
            int usuarioAdministradorId, 
            [FromQuery] int cantidad = 3)
        {
            var response = await _proyectoServicio.ObtenerDestacadosAsync(usuarioAdministradorId, cantidad);
            if (!response.Exitoso)
            {
                return StatusCode(response.CodigoEstado, response);
            }
            return Ok(response);
        }
    }
}
