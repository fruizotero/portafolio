// Controladores/EducacionController.cs
using Microsoft.AspNetCore.Mvc;
using portafolio.backend.API.Dominio.DTOs;
using portafolio.backend.API.Dominio.DTOs.Educacion;
using portafolio.backend.API.Servicios;

namespace portafolio.backend.API.Controladores
{
    [ApiController]
    [Route("[controller]")]
    public class EducacionController : ControllerBase
    {
        private readonly EducacionServicio _educacionServicio;

        public EducacionController(EducacionServicio educacionServicio)
        {
            _educacionServicio = educacionServicio ?? throw new ArgumentNullException(nameof(educacionServicio));
        }

        [HttpGet("usuario/{usuarioAdministradorId}")]
        public async Task<ActionResult<ApiResponseDTO<IEnumerable<EducacionResponseDTO>>>> ObtenerEducacionesPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            var response = await _educacionServicio.ObtenerEducacionesPorUsuarioAdministradorIdAsync(usuarioAdministradorId);
            return StatusCode(response.CodigoEstado, response);
        }

        [HttpGet("{id}/usuario/{usuarioAdministradorId}")]
        public async Task<ActionResult<ApiResponseDTO<EducacionResponseDTO>>> ObtenerEducacionPorIdYUsuarioAdministradorIdAsync(int id, int usuarioAdministradorId)
        {
            var response = await _educacionServicio.ObtenerEducacionPorIdYUsuarioAdministradorIdAsync(id, usuarioAdministradorId);
            return StatusCode(response.CodigoEstado, response);
        }

        [HttpPost("{usuarioAdministradorId}")]
        public async Task<ActionResult<ApiResponseDTO<EducacionResponseDTO>>> CrearEducacion(int usuarioAdministradorId, [FromBody] EducacionRequestDTO educacionRequest)
        {
            var response = await _educacionServicio.CrearEducacionAsync(usuarioAdministradorId, educacionRequest);
            return StatusCode(response.CodigoEstado, response);
        }
    }
}
