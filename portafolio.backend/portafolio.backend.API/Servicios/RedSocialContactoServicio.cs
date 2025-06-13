// Servicios/RedSocialContactoServicio.cs
using portafolio.backend.API.Contexto.Repositorios;
using portafolio.backend.API.Dominio.DTOs;
using portafolio.backend.API.Dominio.DTOs.Imagen;
using portafolio.backend.API.Dominio.DTOs.RedSocialContacto;
using portafolio.backend.API.Dominio.Entidades;
using portafolio.backend.API.Interfaces;

namespace portafolio.backend.API.Servicios
{
    public class RedSocialContactoServicio
    {
        private readonly RedSocialContactoRepositorio _redSocialRepositorio;
        private readonly UsuariosAdministradoresRepositorio _usuariosRepositorio;
        private readonly ServicioImagenes _servicioImagenes;

        public RedSocialContactoServicio(
            RedSocialContactoRepositorio redSocialRepositorio,
            UsuariosAdministradoresRepositorio usuariosRepositorio,
            ServicioImagenes servicioImagenes)
        {
            _redSocialRepositorio = redSocialRepositorio;
            _usuariosRepositorio = usuariosRepositorio;
            _servicioImagenes = servicioImagenes;
        }

        public async Task<ApiResponseDTO<IEnumerable<RedSocialContactoResponseDTO>>> ObtenerRedesSocialesPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            try
            {
                // Verificar si el usuario existe
                var usuario = await _usuariosRepositorio.ObtenerUsuarioAdministradorPorIdAsync(usuarioAdministradorId);
                if (usuario == null)
                {
                    return new ApiResponseDTO<IEnumerable<RedSocialContactoResponseDTO>>
                    {
                        Exitoso = false,
                        Mensaje = "Usuario administrador no encontrado",
                        CodigoEstado = 404
                    };
                }

                var redesSociales = await _redSocialRepositorio.ObtenerRedesSocialesPorUsuarioAdministradorIdAsync(usuarioAdministradorId);

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

                var redesSocialesDTO = redesSociales.Select(MapearRedSocialADTO);

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

        // Método para crear una nueva red social de contacto
        public async Task<ApiResponseDTO<RedSocialContactoResponseDTO>> CrearRedSocialContactoAsync(int usuarioAdministradorId, RedSocialContactoRequestDTO redSocialRequest)
        {
            try
            {
                // Verificar si el usuario existe
                var usuario = await _usuariosRepositorio.ObtenerUsuarioAdministradorPorIdAsync(usuarioAdministradorId);
                if (usuario == null)
                {
                    return new ApiResponseDTO<RedSocialContactoResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "Usuario administrador no encontrado",
                        CodigoEstado = 404
                    };
                }

                // Validar campos obligatorios
                if (string.IsNullOrWhiteSpace(redSocialRequest.Plataforma))
                {
                    return new ApiResponseDTO<RedSocialContactoResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "El nombre de la plataforma es obligatorio",
                        CodigoEstado = 400
                    };
                }

                if (string.IsNullOrWhiteSpace(redSocialRequest.Url))
                {
                    return new ApiResponseDTO<RedSocialContactoResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "La URL es obligatoria",
                        CodigoEstado = 400
                    };
                }

                // Verificar si la URL es válida
                if (!Uri.TryCreate(redSocialRequest.Url, UriKind.Absolute, out _))
                {
                    return new ApiResponseDTO<RedSocialContactoResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "La URL proporcionada no es válida",
                        CodigoEstado = 400
                    };
                }

                // Verificar si ya existe una red social con la misma plataforma para este usuario
                var redesSocialesExistentes = await _redSocialRepositorio.ObtenerRedesSocialesPorUsuarioAdministradorIdAsync(usuarioAdministradorId);
                if (redesSocialesExistentes.Any(r => r.Plataforma.Trim().Equals(redSocialRequest.Plataforma.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    return new ApiResponseDTO<RedSocialContactoResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "Ya existe una red social con esta plataforma",
                        CodigoEstado = 409 // Conflict
                    };
                }

                // Subir icono si se proporcionó
                string? iconUrl = null;
                if (redSocialRequest.Icono != null)
                {
                    try
                    {
                        var iconoResponse = await _servicioImagenes.SubirImagenAsync(
                            new ImagenUploadRequest(redSocialRequest.Icono));
                        
                        if (iconoResponse != null)
                        {
                            iconUrl = iconoResponse.Url;
                        }
                    }
                    catch (Exception ex)
                    {
                        return new ApiResponseDTO<RedSocialContactoResponseDTO>
                        {
                            Exitoso = false,
                            Mensaje = $"Error al subir el icono: {ex.Message}",
                            CodigoEstado = 500
                        };
                    }
                }

                // Crear nueva red social
                var nuevaRedSocial = new RedSocialContacto
                {
                    Plataforma = redSocialRequest.Plataforma.Trim(),
                    Url = redSocialRequest.Url.Trim(),
                    IconUrl = iconUrl,
                    UsuarioAdministradorId = usuarioAdministradorId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                // Guardar en base de datos
                var redSocialCreada = await _redSocialRepositorio.InsertarRedSocialContactoAsync(nuevaRedSocial);

                // Mapear a DTO de respuesta
                var redSocialDTO = MapearRedSocialADTO(redSocialCreada);

                return new ApiResponseDTO<RedSocialContactoResponseDTO>
                {
                    Exitoso = true,
                    Mensaje = "Red social creada correctamente",
                    Datos = redSocialDTO,
                    CodigoEstado = 201 // Created
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<RedSocialContactoResponseDTO>
                {
                    Exitoso = false,
                    Mensaje = $"Error al crear red social: {ex.Message}",
                    CodigoEstado = 500
                };
            }
        }

        private RedSocialContactoResponseDTO MapearRedSocialADTO(RedSocialContacto redSocial)
        {
            return new RedSocialContactoResponseDTO
            {
                Id = redSocial.Id,
                Plataforma = redSocial.Plataforma,
                Url = redSocial.Url,
                IconUrl = redSocial.IconUrl
            };
        }
    }
}
