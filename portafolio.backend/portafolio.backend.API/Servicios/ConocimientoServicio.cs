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

        public async Task<ApiResponseDTO<IEnumerable<ConocimientoResponseDTO>>> ObtenerConocimientosPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            try
            {
                // Verificar si el usuario existe
                var usuario = await _usuariosRepositorio.ObtenerUsuarioAdministradorPorIdAsync(usuarioAdministradorId);
                if (usuario == null)
                {
                    return new ApiResponseDTO<IEnumerable<ConocimientoResponseDTO>>
                    {
                        Exitoso = false,
                        Mensaje = "Usuario administrador no encontrado",
                        CodigoEstado = 404
                    };
                }

                var conocimientos = await _conocimientoRepositorio.ObtenerConocimientosPorUsuarioAdministradorIdAsync(usuarioAdministradorId);

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

                var conocimientosDTO = conocimientos.Select(MapearConocimientoADTO);

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
                var conocimientos = await _conocimientoRepositorio.ObtenerTodosLosConocimientosAsync();

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

        // Método nuevo para crear conocimiento
        public async Task<ApiResponseDTO<ConocimientoResponseDTO>> CrearConocimientoAsync(int usuarioAdministradorId, ConocimientoRequestDTO conocimientoRequest)
        {
            try
            {
                // Verificar si el usuario existe
                var usuario = await _usuariosRepositorio.ObtenerUsuarioAdministradorPorIdAsync(usuarioAdministradorId);
                if (usuario == null)
                {
                    return new ApiResponseDTO<ConocimientoResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "Usuario administrador no encontrado",
                        CodigoEstado = 404
                    };
                }

                // Validar que el nombre no esté vacío
                if (string.IsNullOrWhiteSpace(conocimientoRequest.Nombre))
                {
                    return new ApiResponseDTO<ConocimientoResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "El nombre del conocimiento es obligatorio",
                        CodigoEstado = 400
                    };
                }

                // Verificar si ya existe un conocimiento con el mismo nombre para este usuario
                var conocimientosExistentes = await _conocimientoRepositorio.ObtenerConocimientosPorUsuarioAdministradorIdAsync(usuarioAdministradorId);
                if (conocimientosExistentes.Any(c => c.Nombre.Trim().Equals(conocimientoRequest.Nombre.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    return new ApiResponseDTO<ConocimientoResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "Ya existe un conocimiento con este nombre",
                        CodigoEstado = 409 // Conflict
                    };
                }

                // Crear nuevo conocimiento
                var nuevoConocimiento = new Conocimiento
                {
                    Nombre = conocimientoRequest.Nombre.Trim(),
                    UsuarioAdministradorId = usuarioAdministradorId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                // Guardar en base de datos
                var conocimientoCreado = await _conocimientoRepositorio.InsertarConocimientoAsync(nuevoConocimiento);

                // Mapear a DTO
                var conocimientoDTO = MapearConocimientoADTO(conocimientoCreado);

                return new ApiResponseDTO<ConocimientoResponseDTO>
                {
                    Exitoso = true,
                    Mensaje = "Conocimiento creado correctamente",
                    Datos = conocimientoDTO,
                    CodigoEstado = 201 // Created
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<ConocimientoResponseDTO>
                {
                    Exitoso = false,
                    Mensaje = $"Error al crear conocimiento: {ex.Message}",
                    CodigoEstado = 500
                };
            }
        }

        private ConocimientoResponseDTO MapearConocimientoADTO(Conocimiento conocimiento)
        {
            return new ConocimientoResponseDTO
            {
                Id = conocimiento.Id,
                Nombre = conocimiento.Nombre
            };
        }
    }
}
