using portafolio.backend.API.Contexto.Repositorios;
using portafolio.backend.API.Dominio.DTOs;
using portafolio.backend.API.Dominio.DTOs.Conocimiento;
using portafolio.backend.API.Dominio.DTOs.Habilidad;
using portafolio.backend.API.Dominio.DTOs.Imagen;
using portafolio.backend.API.Dominio.DTOs.Proyecto;
using portafolio.backend.API.Dominio.Entidades;
using portafolio.backend.API.Interfaces;

namespace portafolio.backend.API.Servicios
{
    public class ProyectoServicio
    {
        private readonly ProyectoRepositorio _proyectoRepositorio;
        private readonly UsuariosAdministradoresRepositorio _usuariosRepo;
        private readonly ServicioImagenes _servicioImagenes;
        private readonly HabilidadRepositorio _habilidadRepositorio;
        private readonly ConocimientoRepositorio _conocimientoRepositorio;

        public ProyectoServicio(
            ProyectoRepositorio repo, 
            UsuariosAdministradoresRepositorio usuariosRepo,
            ServicioImagenes servicioImagenes,
            HabilidadRepositorio habilidadRepositorio,
            ConocimientoRepositorio conocimientoRepositorio)
        {
            _proyectoRepositorio = repo;
            _usuariosRepo = usuariosRepo;
            _servicioImagenes = servicioImagenes;
            _habilidadRepositorio = habilidadRepositorio;
            _conocimientoRepositorio = conocimientoRepositorio;
        }

        public async Task<ApiResponseDTO<IEnumerable<ProyectoResponseDTO>>> ObtenerProyectosPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            try
            {
                var usuario = await _usuariosRepo.ObtenerUsuarioAdministradorPorIdAsync(usuarioAdministradorId);
                if (usuario == null)
                {
                    return new ApiResponseDTO<IEnumerable<ProyectoResponseDTO>>
                    {
                        Exitoso = false,
                        Mensaje = "Usuario administrador no encontrado",
                        CodigoEstado = 404
                    };
                }
                
                var proyectos = await _proyectoRepositorio.ObtenerProyectosPorUsuarioAdministradorIdAsync(usuarioAdministradorId);
                
                if (proyectos == null || !proyectos.Any())
                {
                    return new ApiResponseDTO<IEnumerable<ProyectoResponseDTO>>
                    {
                        Exitoso = true,
                        Mensaje = "No se encontraron proyectos para este usuario",
                        CodigoEstado = 200
                    };
                }
                
                var proyectosDTO = proyectos.Select(p => new ProyectoResponseDTO
                {
                    Id = p.Id,
                    Titulo = p.Titulo,
                    Descripcion = p.Descripcion,
                    ImagenDesktopUrl = p.ImagenDesktopUrl,
                    ImagenMobileUrl = p.ImagenMobileUrl,
                    RepositorioUrl = p.RepositorioUrl,
                    LiveUrl = p.LiveUrl,
                    Orden = p.Orden,
                    Habilidades = p.Habilidades.Select(h => new HabilidadResponseDTO
                    {
                        Id = h.Id,
                        Nombre = h.Nombre,
                        EsActual = h.EsActual,
                        LogoUrl = h.LogoUrl,

                    }).ToList(),
                    Conocimientos = p.Conocimientos.Select(c => new ConocimientoResponseDTO
                    {
                        Id = c.Id,
                        Nombre = c.Nombre
                    }).ToList()
                });
                
                return new ApiResponseDTO<IEnumerable<ProyectoResponseDTO>>
                {
                    Exitoso = true,
                    Mensaje = "Proyectos obtenidos correctamente",
                    Datos = proyectosDTO,
                    CodigoEstado = 200
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<IEnumerable<ProyectoResponseDTO>>
                {
                    Exitoso = false,
                    Mensaje = $"Error al obtener proyectos: {ex.Message}",
                    CodigoEstado = 500
                };
            }
        }

        public async Task<ApiResponseDTO<ProyectoResponseDTO>> ObtenerProyectoPorIdAsync(int id, int usuarioAdministradorId)
        {
            try
            {
                var proyecto = await _proyectoRepositorio.ObtenerProyectoPorIdYUsuarioAdministradorIdAsync(id, usuarioAdministradorId);
                
                if (proyecto == null)
                {
                    return new ApiResponseDTO<ProyectoResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "Proyecto no encontrado",
                        CodigoEstado = 404
                    };
                }
                
                var proyectoDTO = new ProyectoResponseDTO
                {
                    Id = proyecto.Id,
                    Titulo = proyecto.Titulo,
                    Descripcion = proyecto.Descripcion,
                    ImagenDesktopUrl = proyecto.ImagenDesktopUrl,
                    ImagenMobileUrl = proyecto.ImagenMobileUrl,
                    RepositorioUrl = proyecto.RepositorioUrl,
                    LiveUrl = proyecto.LiveUrl,
                    Orden = proyecto.Orden,
                    Habilidades = proyecto.Habilidades.Select(h => new HabilidadResponseDTO
                    {
                        Id = h.Id,
                        Nombre = h.Nombre,
                        LogoUrl = h.LogoUrl,
                        EsActual = h.EsActual
                    }).ToList(),
                    Conocimientos = proyecto.Conocimientos.Select(c => new ConocimientoResponseDTO
                    {
                        Id = c.Id,
                        Nombre = c.Nombre
                    }).ToList()
                };
                
                return new ApiResponseDTO<ProyectoResponseDTO>
                {
                    Exitoso = true,
                    Mensaje = "Proyecto obtenido correctamente",
                    Datos = proyectoDTO,
                    CodigoEstado = 200
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<ProyectoResponseDTO>
                {
                    Exitoso = false,
                    Mensaje = $"Error al obtener proyecto: {ex.Message}",
                    CodigoEstado = 500
                };
            }
        }
        
        public async Task<ApiResponseDTO<IEnumerable<ProyectoResponseDTO>>> ObtenerProyectosDestacadosPorUsuarioAdministradorIdAsync(int usuarioAdministradorId, int cantidad = 3)
        {
            try
            {
                // Verificar si el usuario administrador existe
                var usuario = await _usuariosRepo.ObtenerUsuarioAdministradorPorIdAsync(usuarioAdministradorId);
                if (usuario == null)
                {
                    return new ApiResponseDTO<IEnumerable<ProyectoResponseDTO>>
                    {
                        Exitoso = false,
                        Mensaje = "Usuario administrador no encontrado",
                        CodigoEstado = 404
                    };
                }

                var proyectos = await _proyectoRepositorio.ObtenerProyectosDestacadosPorUsuarioAdministradorIdAsync(usuarioAdministradorId, cantidad);

                if (proyectos == null || !proyectos.Any())
                {
                    return new ApiResponseDTO<IEnumerable<ProyectoResponseDTO>>
                    {
                        Exitoso = true,
                        Mensaje = "No se encontraron proyectos destacados",
                        CodigoEstado = 200
                    };
                }
                var proyectosDTO = MapearListaProyectosADTO(proyectos);

                return new ApiResponseDTO<IEnumerable<ProyectoResponseDTO>>
                {
                    Exitoso = true,
                    Mensaje = "Proyectos destacados obtenidos correctamente",
                    Datos = proyectosDTO,
                    CodigoEstado = 200
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<IEnumerable<ProyectoResponseDTO>>
                {
                    Exitoso = false,
                    Mensaje = $"Error al obtener proyectos destacados: {ex.Message}",
                    CodigoEstado = 500
                };
            }
        }

        public async Task<ApiResponseDTO<ProyectoResponseDTO>> CrearProyectoAsync(int usuarioAdministradorId, ProyectoRequestDTO proyectoRequest)
        {
            try
            {
                // Verificar si el usuario administrador existe
                var usuario = await _usuariosRepo.ObtenerUsuarioAdministradorPorIdAsync(usuarioAdministradorId);
                if (usuario == null)
                {
                    return new ApiResponseDTO<ProyectoResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "Usuario administrador no encontrado",
                        CodigoEstado = 404
                    };
                }

                // Subir imágenes si se proporcionaron
                string? imagenDesktopUrl = null;
                if (proyectoRequest.ImagenDesktop != null)
                {
                    var imagenDesktopResponse = await _servicioImagenes.SubirImagenAsync(
                        new ImagenUploadRequest(proyectoRequest.ImagenDesktop));
                    imagenDesktopUrl = imagenDesktopResponse.Url;
                }

                string? imagenMobileUrl = null;
                if (proyectoRequest.ImagenMobile != null)
                {
                    var imagenMobileResponse = await _servicioImagenes.SubirImagenAsync(
                        new ImagenUploadRequest(proyectoRequest.ImagenMobile));
                    imagenMobileUrl = imagenMobileResponse.Url;
                }

                // Crear proyecto
                var nuevoProyecto = new Proyecto
                {
                    Titulo = proyectoRequest.Titulo,
                    Descripcion = proyectoRequest.Descripcion,
                    ImagenDesktopUrl = imagenDesktopUrl,
                    ImagenMobileUrl = imagenMobileUrl,
                    RepositorioUrl = proyectoRequest.RepositorioUrl,
                    LiveUrl = proyectoRequest.LiveUrl,
                    Orden = proyectoRequest.Orden,
                    UsuarioAdministradorId = usuarioAdministradorId,
                    UsuarioAdministrador = usuario,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                // Añadir habilidades al proyecto
                if (proyectoRequest.HabilidadesIds.Any())
                {
                    var habilidades = await _habilidadRepositorio.ObtenerHabilidadesPorUsuarioAdministradorIdAsync(usuarioAdministradorId);
                    foreach (var habilidadId in proyectoRequest.HabilidadesIds)
                    {
                        var habilidad = habilidades.FirstOrDefault(h => h.Id == habilidadId);
                        if (habilidad != null)
                        {
                            nuevoProyecto.Habilidades.Add(habilidad);
                        }
                    }
                }

                // Añadir conocimientos al proyecto
                if (proyectoRequest.ConocimientosIds.Any())
                {
                    var conocimientos = await _conocimientoRepositorio.ObtenerConocimientosPorUsuarioAdministradorIdAsync(usuarioAdministradorId);
                    foreach (var conocimientoId in proyectoRequest.ConocimientosIds)
                    {
                        var conocimiento = conocimientos.FirstOrDefault(c => c.Id == conocimientoId);
                        if (conocimiento != null)
                        {
                            nuevoProyecto.Conocimientos.Add(conocimiento);
                        }
                    }
                }

                // Guardar proyecto
                var proyectoCreado = await _proyectoRepositorio.InsertarProyectoAsync(nuevoProyecto);

                // Cargar proyecto completo con relaciones
                var proyectoCompleto = await _proyectoRepositorio.ObtenerProyectoCompletoConRelacionesPorIdAsync(proyectoCreado.Id);

                if (proyectoCompleto == null)
                {
                    return new ApiResponseDTO<ProyectoResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "Error al cargar el proyecto creado",
                        CodigoEstado = 500
                    };
                }
                ProyectoResponseDTO proyectoDTO = MapearProyectoADTO(proyectoCompleto);

                return new ApiResponseDTO<ProyectoResponseDTO>
                {
                    Exitoso = true,
                    Mensaje = "Proyecto creado correctamente",
                    Datos = proyectoDTO,
                    CodigoEstado = 201
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<ProyectoResponseDTO>
                {
                    Exitoso = false,
                    Mensaje = $"Error al crear proyecto: {ex.Message}",
                    CodigoEstado = 500
                };
            }
        }

        private static ProyectoResponseDTO MapearProyectoADTO(Proyecto proyectoCompleto)
        {

            // Mapear a DTO
            return new ProyectoResponseDTO
            {
                Id = proyectoCompleto.Id,
                Titulo = proyectoCompleto.Titulo,
                Descripcion = proyectoCompleto.Descripcion,
                ImagenDesktopUrl = proyectoCompleto.ImagenDesktopUrl,
                ImagenMobileUrl = proyectoCompleto.ImagenMobileUrl,
                RepositorioUrl = proyectoCompleto.RepositorioUrl,
                LiveUrl = proyectoCompleto.LiveUrl,
                Orden = proyectoCompleto.Orden,
                Habilidades = proyectoCompleto.Habilidades.Select(h => new HabilidadResponseDTO
                {
                    Id = h.Id,
                    Nombre = h.Nombre,
                    LogoUrl = h.LogoUrl,
                    EsActual = h.EsActual
                }).ToList(),
                Conocimientos = proyectoCompleto.Conocimientos.Select(c => new ConocimientoResponseDTO
                {
                    Id = c.Id,
                    Nombre = c.Nombre
                }).ToList()
            };
        }

        private static IEnumerable<ProyectoResponseDTO> MapearListaProyectosADTO(List<Proyecto> proyectos)
        {
            return proyectos.Select(p => new ProyectoResponseDTO
            {
                Id = p.Id,
                Titulo = p.Titulo,
                Descripcion = p.Descripcion,
                ImagenDesktopUrl = p.ImagenDesktopUrl,
                ImagenMobileUrl = p.ImagenMobileUrl,
                RepositorioUrl = p.RepositorioUrl,
                LiveUrl = p.LiveUrl,
                Orden = p.Orden,
                Habilidades = p.Habilidades.Select(h => new HabilidadResponseDTO
                {
                    Id = h.Id,
                    Nombre = h.Nombre,
                    LogoUrl = h.LogoUrl,
                    EsActual = h.EsActual

                }).ToList(),
                Conocimientos = p.Conocimientos.Select(c => new ConocimientoResponseDTO
                {
                    Id = c.Id,
                    Nombre = c.Nombre
                }).ToList()
            });
        }
    }
}
