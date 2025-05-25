using Microsoft.AspNetCore.Mvc;
using portafolio.backend.API.Dominio.DTOs.Autenticacion;
using portafolio.backend.API.Servicios;

namespace portafolio.backend.API.Controladores
{
    [ApiController]
    [Route("[controller]")]
    public class AutenticacionController : ControllerBase
    {

        private readonly AutenticacionServicio _autenticacionServicio;

        public AutenticacionController(AutenticacionServicio autenticacionServicio)
        {
            _autenticacionServicio = autenticacionServicio;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var response = await _autenticacionServicio.Login(loginRequestDTO.Email, loginRequestDTO.Password);

            if (!response.Exitoso)
            {
                return StatusCode(response.CodigoEstado, response);
            }

            return Ok(response);
        }
    }
}
