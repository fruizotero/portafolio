// Controladores/EmpleoController.cs
using Microsoft.AspNetCore.Mvc;
using portafolio.backend.API.Dominio.DTOs;
using portafolio.backend.API.Dominio.DTOs.Empleo;
using portafolio.backend.API.Servicios;

namespace portafolio.backend.API.Controladores
{
    [ApiController]
    [Route("[controller]")]
    public class EmpleoController : ControllerBase
    {
        private readonly EmpleoServicio _empleoServicio;

        public EmpleoController(EmpleoServicio empleoServicio)
        {
            _empleoServicio = empleoServicio ?? throw new ArgumentNullException(nameof(empleoServicio));
        }

        [HttpGet("usuario/{usuarioAdministradorId}")]
        public async Task<ActionResult<ApiResponseDTO<IEnumerable<EmpleoResponseDTO>>>> ObtenerPorUsuario(int usuarioAdministradorId)
        {
            var response = await _empleoServicio.ObtenerPorUsuarioAsync(usuarioAdministradorId);
            if (!response.Exitoso)
            {
                return StatusCode(response.CodigoEstado, response);
            }
            return Ok(response);
        }

        [HttpGet("{id}/usuario/{usuarioAdministradorId}")]
        public async Task<ActionResult<ApiResponseDTO<EmpleoResponseDTO>>> ObtenerPorId(int id, int usuarioAdministradorId)
        {
            var response = await _empleoServicio.ObtenerPorIdAsync(id, usuarioAdministradorId);
            if (!response.Exitoso)
            {
                return StatusCode(response.CodigoEstado, response);
            }
            return Ok(response);
        }
    }
}
