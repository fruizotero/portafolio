using portafolio.backend.API.Contexto.Repositorios;
using portafolio.backend.API.Dominio.DTOs;
using portafolio.backend.API.Dominio.DTOs.Conocimiento;
using portafolio.backend.API.Dominio.Entidades;

namespace portafolio.backend.API.Servicios
{
    public class ConocimientoServicio
    {
        private readonly ConocimientoRepositorio _conocimientoRepositorio;
        private readonly UsuariosAdministradoresRepositorio _usuariosRepositorio;

        public ConocimientoServicio(
            ConocimientoRepositorio conocimientoRepositorio,
            UsuariosAdministradoresRepositorio usuariosRepositorio)
        {
            _conocimientoRepositorio = conocimientoRepositorio;
            _usuariosRepositorio = usuariosRepositorio;
        }

        public async Task<ApiResponseDTO<IEnumerable<ConocimientoResponseDTO>>> ObtenerConocimientosPorUsuarioAsync(int usuarioId)
        {
            try
            {
                // Verificar si el usuario existe
                var usuario = await _usuariosRepositorio.ObtenerPorIdAsync(usuarioId);
                if (usuario == null)
                {
                    return new ApiResponseDTO<IEnumerable<ConocimientoResponseDTO>>
                    {
                        Exitoso = false,
                        Mensaje = "Usuario administrador no encontrado",
                        CodigoEstado = 404
                    };
                }

                var conocimientos = await _conocimientoRepositorio.ObtenerConocimientosPorUsuarioAsync(usuarioId);

                if (conocimientos == null || !conocimientos.Any())
                {
                    return new ApiResponseDTO<IEnumerable<ConocimientoResponseDTO>>
                    {
                        Exitoso = true,
                        Mensaje = "No se encontraron conocimientos para este usuario",
                        CodigoEstado = 200,
                        Datos = new List<ConocimientoResponseDTO>()
                    };
                }

                var conocimientosDTO = conocimientos.Select(c => new ConocimientoResponseDTO
                {
                    Id = c.Id,
                    Nombre = c.Nombre
                });

                return new ApiResponseDTO<IEnumerable<ConocimientoResponseDTO>>
                {
                    Exitoso = true,
                    Mensaje = "Conocimientos obtenidos correctamente",
                    Datos = conocimientosDTO,
                    CodigoEstado = 200
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<IEnumerable<ConocimientoResponseDTO>>
                {
                    Exitoso = false,
                    Mensaje = $"Error al obtener conocimientos: {ex.Message}",
                    CodigoEstado = 500
                };
            }
        }

        public async Task<ApiResponseDTO<ConocimientoResponseDTO>> ObtenerConocimientoPorIdAsync(int id)
        {
            try
            {
                var conocimiento = await _conocimientoRepositorio.ObtenerConocimientoPorIdAsync(id);

                if (conocimiento == null)
                {
                    return new ApiResponseDTO<ConocimientoResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "Conocimiento no encontrado",
                        CodigoEstado = 404
                    };
                }

                var conocimientoDTO = new ConocimientoResponseDTO
                {
                    Id = conocimiento.Id,
                    Nombre = conocimiento.Nombre
                };

                return new ApiResponseDTO<ConocimientoResponseDTO>
                {
                    Exitoso = true,
                    Mensaje = "Conocimiento obtenido correctamente",
                    Datos = conocimientoDTO,
                    CodigoEstado = 200
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<ConocimientoResponseDTO>
                {
                    Exitoso = false,
                    Mensaje = $"Error al obtener conocimiento: {ex.Message}",
                    CodigoEstado = 500
                };
            }
        }

        public async Task<ApiResponseDTO<IEnumerable<ConocimientoResponseDTO>>> ObtenerTodosLosConocimientosAsync()
        {
            try
            {
                var conocimientos = await _conocimientoRepositorio.ObtenerConocimientosAsync();

                if (conocimientos == null || !conocimientos.Any())
                {
                    return new ApiResponseDTO<IEnumerable<ConocimientoResponseDTO>>
                    {
                        Exitoso = true,
                        Mensaje = "No se encontraron conocimientos",
                        CodigoEstado = 200,
                        Datos = new List<ConocimientoResponseDTO>()
                    };
                }

                var conocimientosDTO = conocimientos.Select(c => new ConocimientoResponseDTO
                {
                    Id = c.Id,
                    Nombre = c.Nombre
                });

                return new ApiResponseDTO<IEnumerable<ConocimientoResponseDTO>>
                {
                    Exitoso = true,
                    Mensaje = "Conocimientos obtenidos correctamente",
                    Datos = conocimientosDTO,
                    CodigoEstado = 200
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<IEnumerable<ConocimientoResponseDTO>>
                {
                    Exitoso = false,
                    Mensaje = $"Error al obtener conocimientos: {ex.Message}",
                    CodigoEstado = 500
                };
            }
        }
    }
}
