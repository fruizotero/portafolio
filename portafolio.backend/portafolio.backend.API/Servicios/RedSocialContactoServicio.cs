// Servicios/RedSocialContactoServicio.cs
using portafolio.backend.API.Contexto.Repositorios;
using portafolio.backend.API.Dominio.DTOs;
using portafolio.backend.API.Dominio.DTOs.RedSocialContacto;
using portafolio.backend.API.Dominio.Entidades;

namespace portafolio.backend.API.Servicios
{
    public class RedSocialContactoServicio
    {
        private readonly RedSocialContactoRepositorio _redSocialContactoRepositorio;
        private readonly UsuariosAdministradoresRepositorio _usuariosRepositorio;

        public RedSocialContactoServicio(
            RedSocialContactoRepositorio redSocialContactoRepositorio,
            UsuariosAdministradoresRepositorio usuariosRepositorio)
        {
            _redSocialContactoRepositorio = redSocialContactoRepositorio;
            _usuariosRepositorio = usuariosRepositorio;
        }

        public async Task<ApiResponseDTO<IEnumerable<RedSocialContactoResponseDTO>>> ObtenerPorUsuarioAsync(int usuarioAdministradorId)
        {
            try
            {
                var usuario = await _usuariosRepositorio.ObtenerPorIdAsync(usuarioAdministradorId);
                if (usuario == null)
                {
                    return new ApiResponseDTO<IEnumerable<RedSocialContactoResponseDTO>>
                    {
                        Exitoso = false,
                        Mensaje = "Usuario administrador no encontrado",
                        CodigoEstado = 404
                    };
                }

                var redesSociales = await _redSocialContactoRepositorio.ObtenerPorUsuarioAsync(usuarioAdministradorId);

                if (redesSociales == null || !redesSociales.Any())
                {
                    return new ApiResponseDTO<IEnumerable<RedSocialContactoResponseDTO>>
                    {
                        Exitoso = true,
                        Mensaje = "No se encontraron redes sociales para este usuario",
                        CodigoEstado = 200,
                        Datos = new List<RedSocialContactoResponseDTO>()
                    };
                }

                var redesSocialesDTO = redesSociales.Select(r => new RedSocialContactoResponseDTO
                {
                    Id = r.Id,
                    Plataforma = r.Plataforma,
                    Url = r.Url,
                    IconUrl = r.IconUrl
                });

                return new ApiResponseDTO<IEnumerable<RedSocialContactoResponseDTO>>
                {
                    Exitoso = true,
                    Mensaje = "Redes sociales obtenidas correctamente",
                    Datos = redesSocialesDTO,
                    CodigoEstado = 200
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<IEnumerable<RedSocialContactoResponseDTO>>
                {
                    Exitoso = false,
                    Mensaje = $"Error al obtener redes sociales: {ex.Message}",
                    CodigoEstado = 500
                };
            }
        }

        public async Task<ApiResponseDTO<RedSocialContactoResponseDTO>> ObtenerPorIdAsync(int id, int usuarioAdministradorId)
        {
            try
            {
                // Verificar si el usuario existe
                var usuario = await _usuariosRepositorio.ObtenerPorIdAsync(usuarioAdministradorId);
                if (usuario == null)
                {
                    return new ApiResponseDTO<RedSocialContactoResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "Usuario administrador no encontrado",
                        CodigoEstado = 404
                    };
                }

                var redSocial = await _redSocialContactoRepositorio.ObtenerPorIdAsync(id, usuarioAdministradorId);

                if (redSocial == null)
                {
                    return new ApiResponseDTO<RedSocialContactoResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "Red social no encontrada",
                        CodigoEstado = 404
                    };
                }

                var redSocialDTO = new RedSocialContactoResponseDTO
                {
                    Id = redSocial.Id,
                    Plataforma = redSocial.Plataforma,
                    Url = redSocial.Url,
                    IconUrl = redSocial.IconUrl
                };

                return new ApiResponseDTO<RedSocialContactoResponseDTO>
                {
                    Exitoso = true,
                    Mensaje = "Red social obtenida correctamente",
                    Datos = redSocialDTO,
                    CodigoEstado = 200
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<RedSocialContactoResponseDTO>
                {
                    Exitoso = false,
                    Mensaje = $"Error al obtener red social: {ex.Message}",
                    CodigoEstado = 500
                };
            }
        }
    }
}
