using Microsoft.AspNetCore.Mvc;

namespace portafolio.backend.API.Controladores
{
    public class HabilidadController : Controller
    {
        private readonly Servicios.HabilidadServicio _habilidadServicio;

        public HabilidadController(Servicios.HabilidadServicio habilidadServicio)
        {
            _habilidadServicio = habilidadServicio ?? throw new ArgumentNullException(nameof(habilidadServicio));
        }

        [HttpGet("{usuarioAdministradorId}")]
        public async Task<IActionResult> ObtenerHabilidadesPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            var response = await _habilidadServicio.ObtenerHabilidadesPorUsuarioAdministradorIdAsync(usuarioAdministradorId);
            if (!response.Exitoso)
            {
                return StatusCode(response.CodigoEstado, response);
            }
            return Ok(response);
        }

        [HttpGet("{usuarioAdministradorId}/actuales")]
        public async Task<IActionResult> ObtenerHabilidadesActualesPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            var response = await _habilidadServicio.ObtenerHabilidadesActualesPorUsuarioAdministradorIdAsync(usuarioAdministradorId);
            if (!response.Exitoso)
            {
                return StatusCode(response.CodigoEstado, response);
            }
            return Ok(response);
        }
    }
}
