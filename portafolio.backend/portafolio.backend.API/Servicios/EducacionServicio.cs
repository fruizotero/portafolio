// Servicios/EducacionServicio.cs
using portafolio.backend.API.Contexto.Repositorios;
using portafolio.backend.API.Dominio.DTOs;
using portafolio.backend.API.Dominio.DTOs.Educacion;
using portafolio.backend.API.Dominio.Entidades;

namespace portafolio.backend.API.Servicios
{
    public class EducacionServicio
    {
        private readonly EducacionRepositorio _educacionRepositorio;
        private readonly UsuariosAdministradoresRepositorio _usuariosRepositorio;

        public EducacionServicio(
            EducacionRepositorio educacionRepositorio,
            UsuariosAdministradoresRepositorio usuariosRepositorio)
        {
            _educacionRepositorio = educacionRepositorio;
            _usuariosRepositorio = usuariosRepositorio;
        }

        public async Task<ApiResponseDTO<IEnumerable<EducacionResponseDTO>>> ObtenerEducacionesPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            try
            {
                // Verificar si el usuario existe
                var usuario = await _usuariosRepositorio.ObtenerUsuarioAdministradorPorIdAsync(usuarioAdministradorId);
                if (usuario == null)
                {
                    return new ApiResponseDTO<IEnumerable<EducacionResponseDTO>>
                    {
                        Exitoso = false,
                        Mensaje = "Usuario administrador no encontrado",
                        CodigoEstado = 404
                    };
                }

                var educaciones = await _educacionRepositorio.ObtenerEducacionesPorUsuarioAdministradorIdAsync(usuarioAdministradorId);

                if (educaciones == null || !educaciones.Any())
                {
                    return new ApiResponseDTO<IEnumerable<EducacionResponseDTO>>
                    {
                        Exitoso = true,
                        Mensaje = "No se encontraron registros de educaci�n para este usuario",
                        CodigoEstado = 200,
                        Datos = new List<EducacionResponseDTO>()
                    };
                }

                var educacionesDTO = educaciones.Select(MapearEducacionADTO);

                return new ApiResponseDTO<IEnumerable<EducacionResponseDTO>>
                {
                    Exitoso = true,
                    Mensaje = "Registros de educaci�n obtenidos correctamente",
                    Datos = educacionesDTO,
                    CodigoEstado = 200
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<IEnumerable<EducacionResponseDTO>>
                {
                    Exitoso = false,
                    Mensaje = $"Error al obtener registros de educaci�n: {ex.Message}",
                    CodigoEstado = 500
                };
            }
        }

        public async Task<ApiResponseDTO<EducacionResponseDTO>> ObtenerEducacionPorIdYUsuarioAdministradorIdAsync(int id, int usuarioAdministradorId)
        {
            try
            {
                var educacion = await _educacionRepositorio.ObtenerEducacionPorIdYUsuarioAdministradorIdAsync(id, usuarioAdministradorId);

                if (educacion == null)
                {
                    return new ApiResponseDTO<EducacionResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "Registro de educaci�n no encontrado",
                        CodigoEstado = 404
                    };
                }

                var educacionDTO = MapearEducacionADTO(educacion);

                return new ApiResponseDTO<EducacionResponseDTO>
                {
                    Exitoso = true,
                    Mensaje = "Registro de educaci�n obtenido correctamente",
                    Datos = educacionDTO,
                    CodigoEstado = 200
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<EducacionResponseDTO>
                {
                    Exitoso = false,
                    Mensaje = $"Error al obtener registro de educaci�n: {ex.Message}",
                    CodigoEstado = 500
                };
            }
        }

        // M�todo para crear un nuevo registro de educaci�n
        public async Task<ApiResponseDTO<EducacionResponseDTO>> CrearEducacionAsync(int usuarioAdministradorId, EducacionRequestDTO educacionRequest)
        {
            try
            {
                // Verificar si el usuario existe
                var usuario = await _usuariosRepositorio.ObtenerUsuarioAdministradorPorIdAsync(usuarioAdministradorId);
                if (usuario == null)
                {
                    return new ApiResponseDTO<EducacionResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "Usuario administrador no encontrado",
                        CodigoEstado = 404
                    };
                }

                // Validar campos obligatorios
                if (string.IsNullOrWhiteSpace(educacionRequest.Institucion))
                {
                    return new ApiResponseDTO<EducacionResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "El nombre de la instituci�n es obligatorio",
                        CodigoEstado = 400
                    };
                }

                if (string.IsNullOrWhiteSpace(educacionRequest.Titulo))
                {
                    return new ApiResponseDTO<EducacionResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "El t�tulo obtenido es obligatorio",
                        CodigoEstado = 400
                    };
                }

                // Validar fechas
                if (educacionRequest.FechaInicio > DateTime.Now)
                {
                    return new ApiResponseDTO<EducacionResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "La fecha de inicio no puede ser futura",
                        CodigoEstado = 400
                    };
                }

                if (educacionRequest.FechaFin.HasValue && educacionRequest.FechaFin.Value < educacionRequest.FechaInicio)
                {
                    return new ApiResponseDTO<EducacionResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "La fecha de finalizaci�n no puede ser anterior a la fecha de inicio",
                        CodigoEstado = 400
                    };
                }

                // Verificar si ya existe un registro con la misma instituci�n y t�tulo para este usuario
                var educacionesExistentes = await _educacionRepositorio.ObtenerEducacionesPorUsuarioAdministradorIdAsync(usuarioAdministradorId);
                if (educacionesExistentes.Any(e => 
                    e.Institucion.Trim().Equals(educacionRequest.Institucion.Trim(), StringComparison.OrdinalIgnoreCase) && 
                    e.Titulo.Trim().Equals(educacionRequest.Titulo.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    return new ApiResponseDTO<EducacionResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "Ya existe un registro de educaci�n con esta instituci�n y t�tulo",
                        CodigoEstado = 409 // Conflict
                    };
                }

                // Crear nuevo registro de educaci�n
                var nuevaEducacion = new Educacion
                {
                    Institucion = educacionRequest.Institucion.Trim(),
                    Titulo = educacionRequest.Titulo.Trim(),
                    FechaInicio = educacionRequest.FechaInicio,
                    FechaFin = educacionRequest.FechaFin,
                    Descripcion = educacionRequest.Descripcion?.Trim(),
                    UsuarioAdministradorId = usuarioAdministradorId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                // Guardar en base de datos
                var educacionCreada = await _educacionRepositorio.InsertarEducacionAsync(nuevaEducacion);

                // Mapear a DTO de respuesta
                var educacionDTO = MapearEducacionADTO(educacionCreada);

                return new ApiResponseDTO<EducacionResponseDTO>
                {
                    Exitoso = true,
                    Mensaje = "Registro de educaci�n creado correctamente",
                    Datos = educacionDTO,
                    CodigoEstado = 201 // Created
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<EducacionResponseDTO>
                {
                    Exitoso = false,
                    Mensaje = $"Error al crear registro de educaci�n: {ex.Message}",
                    CodigoEstado = 500
                };
            }
        }

        // M�todo para eliminar una educaci�n
        public async Task<ApiResponseDTO<string>> EliminarEducacionAsync(int id, int usuarioAdministradorId)
        {
            try
            {
                // Verificar si el usuario existe
                var usuario = await _usuariosRepositorio.ObtenerUsuarioAdministradorPorIdAsync(usuarioAdministradorId);
                if (usuario == null)
                {
                    return new ApiResponseDTO<string>
                    {
                        Exitoso = false,
                        Mensaje = "Usuario administrador no encontrado",
                        CodigoEstado = 404
                    };
                }

                // Verificar si la educaci�n existe y pertenece al usuario
                var educacion = await _educacionRepositorio.ObtenerEducacionPorIdYUsuarioAdministradorIdAsync(id, usuarioAdministradorId);
                if (educacion == null)
                {
                    return new ApiResponseDTO<string>
                    {
                        Exitoso = false,
                        Mensaje = "Registro de educaci�n no encontrado o no pertenece al usuario",
                        CodigoEstado = 404
                    };
                }

                // Eliminar la educaci�n
                var resultado = await _educacionRepositorio.EliminarEducacionAsync(id);
                if (!resultado)
                {
                    return new ApiResponseDTO<string>
                    {
                        Exitoso = false,
                        Mensaje = "Error al eliminar el registro de educaci�n",
                        CodigoEstado = 500
                    };
                }

                return new ApiResponseDTO<string>
                {
                    Exitoso = true,
                    Mensaje = "Registro de educaci�n eliminado correctamente",
                    CodigoEstado = 200
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<string>
                {
                    Exitoso = false,
                    Mensaje = $"Error al eliminar registro de educaci�n: {ex.Message}",
                    CodigoEstado = 500
                };
            }
        }

        private EducacionResponseDTO MapearEducacionADTO(Educacion educacion)
        {
            return new EducacionResponseDTO
            {
                Id = educacion.Id,
                Institucion = educacion.Institucion,
                Titulo = educacion.Titulo,
                FechaInicio = educacion.FechaInicio,
                FechaFin = educacion.FechaFin,
                Descripcion = educacion.Descripcion
            };
        }
    }
}
