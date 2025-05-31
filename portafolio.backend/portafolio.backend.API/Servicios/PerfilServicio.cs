using portafolio.backend.API.Contexto.Repositorios;
using portafolio.backend.API.Dominio.DTOs;
using portafolio.backend.API.Dominio.DTOs.Perfil;
using portafolio.backend.API.Dominio.Entidades;

namespace portafolio.backend.API.Servicios
{
    public class PerfilServicio
    {
        private readonly PerfilRespositorio _perfilRepositorio;
        public PerfilServicio(PerfilRespositorio perfilRepositorio)
        {
            _perfilRepositorio = perfilRepositorio ?? throw new ArgumentNullException(nameof(perfilRepositorio));
        }
        public async Task<ApiResponseDTO<PerfilResponseDTO>> ObtenerPerfilPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
          var response = new ApiResponseDTO<PerfilResponseDTO>
          {
              Exitoso = false,
              Mensaje = "Perfil no encontrado",
              CodigoEstado = 404 // Not Found
          };

            var perfilEncontrado = await _perfilRepositorio.ObtenerPerfilPorUsuarioAdministradorIdAsync(usuarioAdministradorId);
            if (perfilEncontrado != null)
            {
                response.Exitoso = true;
                response.Mensaje = "Perfil encontrado";
                response.Datos = new PerfilResponseDTO
                {
                    AcercaDeMi = perfilEncontrado.AcercaDeMi,
                    Apellidos = perfilEncontrado.Apellidos,
                    Descripcion = perfilEncontrado.Descripcion,
                    FotoURL = perfilEncontrado.FotoURL,
                    Nombre = perfilEncontrado.Nombre,
                    Saludo = perfilEncontrado.Saludo
                };
                response.CodigoEstado = 200; // OK

            }



                return response;
        }
    }
}
