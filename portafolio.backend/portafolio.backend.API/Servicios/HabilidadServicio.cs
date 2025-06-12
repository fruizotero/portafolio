using portafolio.backend.API.Contexto.Repositorios;
using portafolio.backend.API.Dominio.DTOs;
using portafolio.backend.API.Dominio.DTOs.Habilidad;
using portafolio.backend.API.Dominio.DTOs.Imagen;
using portafolio.backend.API.Dominio.Entidades;
using portafolio.backend.API.Interfaces;

namespace portafolio.backend.API.Servicios
{
    public class HabilidadServicio
    {
        private readonly HabilidadRepositorio _habilidadRepositorio;
        private readonly UsuariosAdministradoresRepositorio _usuariosRepositorio;
        private readonly ServicioImagenes _servicioImagenes;

        public HabilidadServicio(
            HabilidadRepositorio habilidadRepositorio, 
            UsuariosAdministradoresRepositorio usuariosRepositorio,
            ServicioImagenes servicioImagenes)
        {
            _habilidadRepositorio = habilidadRepositorio;
            _usuariosRepositorio = usuariosRepositorio;
            _servicioImagenes = servicioImagenes;
        }

        public async Task<ApiResponseDTO<IEnumerable<HabilidadResponseDTO>>> ObtenerHabilidadesPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            try
            {
                
                var usuarioAdministrador = await _usuariosRepositorio.ObtenerUsuarioAdministradorPorIdAsync(usuarioAdministradorId);
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
                        Exitoso = true,
                        Mensaje = "No se encontraron habilidades para este usuario",
                        CodigoEstado = 200,
                        Datos= new List<HabilidadResponseDTO>()
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
                        Exitoso = true,
                        Mensaje = "No se encontraron habilidades actuales para este usuario",
                        CodigoEstado = 200,
                        Datos = new List<HabilidadResponseDTO>()
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

        // Método para crear una nueva habilidad
        public async Task<ApiResponseDTO<HabilidadResponseDTO>> CrearHabilidadAsync(int usuarioAdministradorId, HabilidadRequestDTO habilidadRequest)
        {
            try
            {
                // Verificar si el usuario existe
                var usuario = await _usuariosRepositorio.ObtenerUsuarioAdministradorPorIdAsync(usuarioAdministradorId);
                if (usuario == null)
                {
                    return new ApiResponseDTO<HabilidadResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "Usuario administrador no encontrado",
                        CodigoEstado = 404
                    };
                }

                // Validar que el nombre no esté vacío
                if (string.IsNullOrWhiteSpace(habilidadRequest.Nombre))
                {
                    return new ApiResponseDTO<HabilidadResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "El nombre de la habilidad es obligatorio",
                        CodigoEstado = 400
                    };
                }

                // Verificar si ya existe una habilidad con el mismo nombre para este usuario
                var habilidadesExistentes = await _habilidadRepositorio.ObtenerHabilidadesPorUsuarioAdministradorIdAsync(usuarioAdministradorId);
                if (habilidadesExistentes.Any(h => h.Nombre.Trim().Equals(habilidadRequest.Nombre.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    return new ApiResponseDTO<HabilidadResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "Ya existe una habilidad con este nombre",
                        CodigoEstado = 409 // Conflict
                    };
                }

                // Subir logo si se proporcionó
                string? logoUrl = null;
                if (habilidadRequest.Logo != null)
                {
                    try
                    {
                        var logoResponse = await _servicioImagenes.SubirImagenAsync(
                            new ImagenUploadRequest(habilidadRequest.Logo));
                        
                        if (logoResponse != null)
                        {
                            logoUrl = logoResponse.Url;
                        }
                    }
                    catch (Exception ex)
                    {
                        return new ApiResponseDTO<HabilidadResponseDTO>
                        {
                            Exitoso = false,
                            Mensaje = $"Error al subir la imagen: {ex.Message}",
                            CodigoEstado = 500
                        };
                    }
                }

                // Crear nueva habilidad
                var nuevaHabilidad = new Habilidad
                {
                    Nombre = habilidadRequest.Nombre.Trim(),
                    EsActual = habilidadRequest.EsActual,
                    LogoUrl = logoUrl,
                    UsuarioAdministradorId = usuarioAdministradorId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                // Guardar en base de datos
                var habilidadCreada = await _habilidadRepositorio.InsertarHabilidadAsync(nuevaHabilidad);

                // Mapear a DTO de respuesta
                var habilidadDTO = MapearHabilidadADTO(habilidadCreada);

                return new ApiResponseDTO<HabilidadResponseDTO>
                {
                    Exitoso = true,
                    Mensaje = "Habilidad creada correctamente",
                    Datos = habilidadDTO,
                    CodigoEstado = 201 // Created
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<HabilidadResponseDTO>
                {
                    Exitoso = false,
                    Mensaje = $"Error al crear habilidad: {ex.Message}",
                    CodigoEstado = 500
                };
            }
        }

        private HabilidadResponseDTO MapearHabilidadADTO(Habilidad habilidad)
        {
            return new HabilidadResponseDTO
            {
                Id = habilidad.Id,
                Nombre = habilidad.Nombre,
                LogoUrl = habilidad.LogoUrl,
                EsActual = habilidad.EsActual
            };
        }
    }
}
