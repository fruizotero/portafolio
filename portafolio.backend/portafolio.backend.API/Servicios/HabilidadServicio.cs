using portafolio.backend.API.Contexto.Repositorios;
using portafolio.backend.API.Dominio.DTOs.Habilidad;
using portafolio.backend.API.Dominio.DTOs;

namespace portafolio.backend.API.Servicios
{
    public class HabilidadServicio
    {

        private readonly HabilidadRepositorio _habilidadRepositorio;
        private readonly UsuariosAdministradoresRepositorio _usuariosAdministradoresRepositorio;

        public HabilidadServicio(HabilidadRepositorio habilidadRepositorio, UsuariosAdministradoresRepositorio usuariosAdministradoresRepositorio)
        {
            _habilidadRepositorio = habilidadRepositorio;
            _usuariosAdministradoresRepositorio = usuariosAdministradoresRepositorio;
        }

        public async Task<ApiResponseDTO<IEnumerable<HabilidadResponseDTO>>> ObtenerHabilidadesPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            try
            {
                
                var usuarioAdministrador = await _usuariosAdministradoresRepositorio.ObtenerPorIdAsync(usuarioAdministradorId);
                if (usuarioAdministrador == null)
                {
                    return new ApiResponseDTO<IEnumerable<HabilidadResponseDTO>>
                    {
                        Exitoso = false,
                        Mensaje = "Usuario administrador no encontrado",
                        CodigoEstado = 404
                    };
                }
                var habilidades = await _habilidadRepositorio.ObtenerHabilidadesPorUsuarioAdministradorIdAsync(usuarioAdministradorId);

                if (habilidades == null || !habilidades.Any())
                {
                    return new ApiResponseDTO<IEnumerable<HabilidadResponseDTO>>
                    {
                        Exitoso = false,
                        Mensaje = "No se encontraron habilidades para este usuario",
                        CodigoEstado = 404
                    };
                }

                var habilidadesDTO = habilidades.Select(h => new HabilidadResponseDTO
                {
                    Id = h.Id,
                    Nombre = h.Nombre,
                    LogoUrl = h.LogoUrl,
                    EsActual = h.EsActual
                });

                return new ApiResponseDTO<IEnumerable<HabilidadResponseDTO>>
                {
                    Exitoso = true,
                    Mensaje = "Habilidades obtenidas correctamente",
                    Datos = habilidadesDTO,
                    CodigoEstado = 200
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<IEnumerable<HabilidadResponseDTO>>
                {
                    Exitoso = false,
                    Mensaje = $"Error al obtener las habilidades: {ex.Message}",
                    CodigoEstado = 500
                };
            }
        }

        public async Task<ApiResponseDTO<IEnumerable<HabilidadResponseDTO>>> ObtenerHabilidadesActualesPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            try
            {
                var habilidades = await _habilidadRepositorio.ObtenerHabilidadesActualesPorUsuarioAdministradorIdAsync(usuarioAdministradorId);

                if (habilidades == null || !habilidades.Any())
                {
                    return new ApiResponseDTO<IEnumerable<HabilidadResponseDTO>>
                    {
                        Exitoso = false,
                        Mensaje = "No se encontraron habilidades actuales para este usuario",
                        CodigoEstado = 404
                    };
                }

                var habilidadesDTO = habilidades.Select(h => new HabilidadResponseDTO
                {
                    Id = h.Id,
                    Nombre = h.Nombre,
                    LogoUrl = h.LogoUrl,
                    EsActual = h.EsActual
                });

                return new ApiResponseDTO<IEnumerable<HabilidadResponseDTO>>
                {
                    Exitoso = true,
                    Mensaje = "Habilidades actuales obtenidas correctamente",
                    Datos = habilidadesDTO,
                    CodigoEstado = 200
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<IEnumerable<HabilidadResponseDTO>>
                {
                    Exitoso = false,
                    Mensaje = $"Error al obtener las habilidades actuales: {ex.Message}",
                    CodigoEstado = 500
                };
            }
        }
    }
}
