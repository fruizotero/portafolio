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
        private readonly RedSocialContactoServicio _redSocialContactoServicio;

        public RedSocialContactoController(RedSocialContactoServicio redSocialContactoServicio)
        {
            _redSocialContactoServicio = redSocialContactoServicio ?? throw new ArgumentNullException(nameof(redSocialContactoServicio));
        }

        [HttpGet("usuario/{usuarioAdministradorId}")]
        public async Task<ActionResult<ApiResponseDTO<IEnumerable<RedSocialContactoResponseDTO>>>> ObtenerPorUsuario(int usuarioAdministradorId)
        {
            var response = await _redSocialContactoServicio.ObtenerPorUsuarioAsync(usuarioAdministradorId);
            if (!response.Exitoso)
            {
                return StatusCode(response.CodigoEstado, response);
            }
            return Ok(response);
        }

        [HttpGet("{id}/usuario/{usuarioAdministradorId}")]
        public async Task<ActionResult<ApiResponseDTO<RedSocialContactoResponseDTO>>> ObtenerPorId(int id, int usuarioAdministradorId)
        {
            var response = await _redSocialContactoServicio.ObtenerPorIdAsync(id, usuarioAdministradorId);
            if (!response.Exitoso)
            {
                return StatusCode(response.CodigoEstado, response);
            }
            return Ok(response);
        }
    }
}
