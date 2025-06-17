using Microsoft.IdentityModel.Tokens;
using portafolio.backend.API.Dominio.Entidades;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace portafolio.backend.API.Utilidades
{
    public class JWTHelper
    {
        private readonly IConfiguration _configuration;
        // 1 día = 1440 minutos
        private const int DuracionDeTokenEnMinutos = 1440;

        public JWTHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual string GenerateToken(UsuarioAdministrador administrador)
        {

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
           {

                new Claim(ClaimTypes.Email, administrador.Email),
                new Claim(ClaimTypes.Role, "Administrador")

            };

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(DuracionDeTokenEnMinutos),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
