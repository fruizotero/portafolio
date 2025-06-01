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
        public async Task<ActionResult<ApiResponseDTO<IEnumerable<EducacionResponseDTO>>>> ObtenerPorUsuario(int usuarioAdministradorId)
        {
            var response = await _educacionServicio.ObtenerPorUsuarioAsync(usuarioAdministradorId);
            if (!response.Exitoso)
            {
                return StatusCode(response.CodigoEstado, response);
            }
            return Ok(response);
        }

        [HttpGet("{id}/usuario/{usuarioAdministradorId}")]
        public async Task<ActionResult<ApiResponseDTO<EducacionResponseDTO>>> ObtenerPorId(int id, int usuarioAdministradorId)
        {
            var response = await _educacionServicio.ObtenerPorIdAsync(id, usuarioAdministradorId);
            if (!response.Exitoso)
            {
                return StatusCode(response.CodigoEstado, response);
            }
            return Ok(response);
        }

        

       
    }
}
