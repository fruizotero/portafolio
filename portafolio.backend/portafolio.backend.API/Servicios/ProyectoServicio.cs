using portafolio.backend.API.Contexto.Repositorios;
using portafolio.backend.API.Dominio.DTOs;
using portafolio.backend.API.Dominio.DTOs.Conocimiento;
using portafolio.backend.API.Dominio.DTOs.Habilidad;
using portafolio.backend.API.Dominio.DTOs.Proyecto;
using portafolio.backend.API.Dominio.Entidades;

namespace portafolio.backend.API.Servicios
{
    public class ProyectoServicio
    {
        private readonly ProyectoRepositorio _proyectoRepositorio;
        private readonly UsuariosAdministradoresRepositorio _usuariosRepo;
        
        public ProyectoServicio(ProyectoRepositorio repo, UsuariosAdministradoresRepositorio usuariosRepo)
        {
            _proyectoRepositorio = repo;
            _usuariosRepo = usuariosRepo;
        }

        public async Task<ApiResponseDTO<IEnumerable<ProyectoResponseDTO>>> ObtenerPorUsuarioAsync(int usuarioAdministradorId)
        {
            try
            {
                var usuario = await _usuariosRepo.ObtenerPorIdAsync(usuarioAdministradorId);
                if (usuario == null)
                {
                    return new ApiResponseDTO<IEnumerable<ProyectoResponseDTO>>
                    {
                        Exitoso = false,
                        Mensaje = "Usuario administrador no encontrado",
                        CodigoEstado = 404
                    };
                }
                
                var proyectos = await _proyectoRepositorio.ObtenerPorUsuario(usuarioAdministradorId);
                
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

        public async Task<ApiResponseDTO<ProyectoResponseDTO>> ObtenerPorIdAsync(int id, int usuarioAdministradorId)
        {
            try
            {
                var proyecto = await _proyectoRepositorio.ObtenerPorId(id, usuarioAdministradorId);
                
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
        
        public async Task<ApiResponseDTO<IEnumerable<ProyectoResponseDTO>>> ObtenerDestacadosAsync(int usuarioAdministradorId, int cantidad = 3)
        {
            try
            {
                // Verificar si el usuario administrador existe
                var usuario = await _usuariosRepo.ObtenerPorIdAsync(usuarioAdministradorId);
                if (usuario == null)
                {
                    return new ApiResponseDTO<IEnumerable<ProyectoResponseDTO>>
                    {
                        Exitoso = false,
                        Mensaje = "Usuario administrador no encontrado",
                        CodigoEstado = 404
                    };
                }

                var proyectos = await _proyectoRepositorio.ObtenerDestacados(usuarioAdministradorId, cantidad);

                if (proyectos == null || !proyectos.Any())
                {
                    return new ApiResponseDTO<IEnumerable<ProyectoResponseDTO>>
                    {
                        Exitoso = true,
                        Mensaje = "No se encontraron proyectos destacados",
                        CodigoEstado = 200
                    };
                }
                var proyectosDTO = MapearProyectosADTO(proyectos);

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

        private static IEnumerable<ProyectoResponseDTO> MapearProyectosADTO(List<Proyecto> proyectos)
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
