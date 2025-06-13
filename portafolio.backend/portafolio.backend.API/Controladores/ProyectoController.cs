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
        public async Task<ActionResult<ApiResponseDTO<IEnumerable<ProyectoResponseDTO>>>> ObtenerProyectosPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            var response = await _proyectoServicio.ObtenerProyectosPorUsuarioAdministradorIdAsync(usuarioAdministradorId);
            return StatusCode(response.CodigoEstado, response);
        }

        [HttpGet("usuario/{usuarioAdministradorId}/destacados/{cantidad=3}")]
        public async Task<ActionResult<ApiResponseDTO<IEnumerable<ProyectoResponseDTO>>>> ObtenerProyectosDestacadosPorUsuarioAdministradorIdAsync(int usuarioAdministradorId, int cantidad = 3)
        {
            var response = await _proyectoServicio.ObtenerProyectosDestacadosPorUsuarioAdministradorIdAsync(usuarioAdministradorId, cantidad);
            return StatusCode(response.CodigoEstado, response);
        }

        [HttpGet("{id}/usuario/{usuarioAdministradorId}")]
        public async Task<ActionResult<ApiResponseDTO<ProyectoResponseDTO>>> ObtenerProyectoPorIdYUsuarioAdministradorIdAsync(int id, int usuarioAdministradorId)
        {
            var response = await _proyectoServicio.ObtenerProyectoPorIdAsync(id, usuarioAdministradorId);
            return StatusCode(response.CodigoEstado, response);
        }

        [HttpPost("{usuarioAdministradorId}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<ApiResponseDTO<ProyectoResponseDTO>>> CrearProyecto(int usuarioAdministradorId, [FromForm] ProyectoRequestDTO proyectoRequest)
        {
            var response = await _proyectoServicio.CrearProyectoAsync(usuarioAdministradorId, proyectoRequest);
            return StatusCode(response.CodigoEstado, response);
        }

        [HttpDelete("{id}/usuario/{usuarioAdministradorId}")]
        public async Task<ActionResult<ApiResponseDTO<string>>> EliminarProyecto(int id, int usuarioAdministradorId)
        {
            var response = await _proyectoServicio.EliminarProyectoAsync(id, usuarioAdministradorId);
            return StatusCode(response.CodigoEstado, response);
        }
    }
}
