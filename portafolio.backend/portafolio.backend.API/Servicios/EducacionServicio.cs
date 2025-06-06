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

        public async Task<ApiResponseDTO<IEnumerable<EducacionResponseDTO>>> ObtenerPorUsuarioAsync(int usuarioAdministradorId)
        {
            try
            {
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
                        Mensaje = "No se encontraron registros de educación para este usuario",
                        CodigoEstado = 200,
                        Datos = new List<EducacionResponseDTO>()
                    };
                }

                var educacionesDTO = educaciones.Select(e => new EducacionResponseDTO
                {
                    Id = e.Id,
                    Institucion = e.Institucion,
                    Titulo = e.Titulo,
                    FechaInicio = e.FechaInicio,
                    FechaFin = e.FechaFin,
                    Descripcion = e.Descripcion
                });

                return new ApiResponseDTO<IEnumerable<EducacionResponseDTO>>
                {
                    Exitoso = true,
                    Mensaje = "Registros de educación obtenidos correctamente",
                    Datos = educacionesDTO,
                    CodigoEstado = 200
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<IEnumerable<EducacionResponseDTO>>
                {
                    Exitoso = false,
                    Mensaje = $"Error al obtener registros de educación: {ex.Message}",
                    CodigoEstado = 500
                };
            }
        }

        public async Task<ApiResponseDTO<EducacionResponseDTO>> ObtenerPorIdAsync(int id, int usuarioAdministradorId)
        {
            try
            {
                var educacion = await _educacionRepositorio.ObtenerEducacionPorIdYUsuarioAdministradorIdAsync(id, usuarioAdministradorId);

                if (educacion == null)
                {
                    return new ApiResponseDTO<EducacionResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "Registro de educación no encontrado",
                        CodigoEstado = 404
                    };
                }

                var educacionDTO = new EducacionResponseDTO
                {
                    Id = educacion.Id,
                    Institucion = educacion.Institucion,
                    Titulo = educacion.Titulo,
                    FechaInicio = educacion.FechaInicio,
                    FechaFin = educacion.FechaFin,
                    Descripcion = educacion.Descripcion
                };

                return new ApiResponseDTO<EducacionResponseDTO>
                {
                    Exitoso = true,
                    Mensaje = "Registro de educación obtenido correctamente",
                    Datos = educacionDTO,
                    CodigoEstado = 200
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<EducacionResponseDTO>
                {
                    Exitoso = false,
                    Mensaje = $"Error al obtener registro de educación: {ex.Message}",
                    CodigoEstado = 500
                };
            }
        }

         }
}
