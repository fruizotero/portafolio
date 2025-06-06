// Servicios/EmpleoServicio.cs
using portafolio.backend.API.Contexto.Repositorios;
using portafolio.backend.API.Dominio.DTOs;
using portafolio.backend.API.Dominio.DTOs.Empleo;
using portafolio.backend.API.Dominio.Entidades;

namespace portafolio.backend.API.Servicios
{
    public class EmpleoServicio
    {
        private readonly EmpleoRepositorio _empleoRepositorio;
        private readonly UsuariosAdministradoresRepositorio _usuariosRepositorio;

        public EmpleoServicio(
            EmpleoRepositorio empleoRepositorio,
            UsuariosAdministradoresRepositorio usuariosRepositorio)
        {
            _empleoRepositorio = empleoRepositorio;
            _usuariosRepositorio = usuariosRepositorio;
        }

        public async Task<ApiResponseDTO<IEnumerable<EmpleoResponseDTO>>> ObtenerEmpleosPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            try
            {
                var usuario = await _usuariosRepositorio.ObtenerUsuarioAdministradorPorIdAsync(usuarioAdministradorId);
                if (usuario == null)
                {
                    return new ApiResponseDTO<IEnumerable<EmpleoResponseDTO>>
                    {
                        Exitoso = false,
                        Mensaje = "Usuario administrador no encontrado",
                        CodigoEstado = 404
                    };
                }

                var empleos = await _empleoRepositorio.ObtenerEmpleosPorUsuarioAdministradorIdAsync(usuarioAdministradorId);

                if (empleos == null || !empleos.Any())
                {
                    return new ApiResponseDTO<IEnumerable<EmpleoResponseDTO>>
                    {
                        Exitoso = true,
                        Mensaje = "No se encontraron registros de empleo para este usuario",
                        CodigoEstado = 200,
                        Datos = new List<EmpleoResponseDTO>()
                    };
                }

                var empleosDTO = empleos.Select(e => new EmpleoResponseDTO
                {
                    Id = e.Id,
                    Empresa = e.Empresa,
                    Cargo = e.Cargo,
                    FechaInicio = e.FechaInicio,
                    FechaFin = e.FechaFin,
                    Descripcion = e.Descripcion
                });

                return new ApiResponseDTO<IEnumerable<EmpleoResponseDTO>>
                {
                    Exitoso = true,
                    Mensaje = "Registros de empleo obtenidos correctamente",
                    Datos = empleosDTO,
                    CodigoEstado = 200
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<IEnumerable<EmpleoResponseDTO>>
                {
                    Exitoso = false,
                    Mensaje = $"Error al obtener registros de empleo: {ex.Message}",
                    CodigoEstado = 500
                };
            }
        }

        public async Task<ApiResponseDTO<EmpleoResponseDTO>> ObtenerEmpleoPorIdAsync(int id, int usuarioAdministradorId)
        {
            try
            {
                var empleo = await _empleoRepositorio.ObtenerEmpleoPorIdYUsuarioAdministradorIdAsync(id, usuarioAdministradorId);

                if (empleo == null)
                {
                    return new ApiResponseDTO<EmpleoResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "Registro de empleo no encontrado",
                        CodigoEstado = 404
                    };
                }

                var empleoDTO = new EmpleoResponseDTO
                {
                    Id = empleo.Id,
                    Empresa = empleo.Empresa,
                    Cargo = empleo.Cargo,
                    FechaInicio = empleo.FechaInicio,
                    FechaFin = empleo.FechaFin,
                    Descripcion = empleo.Descripcion
                };

                return new ApiResponseDTO<EmpleoResponseDTO>
                {
                    Exitoso = true,
                    Mensaje = "Registro de empleo obtenido correctamente",
                    Datos = empleoDTO,
                    CodigoEstado = 200
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<EmpleoResponseDTO>
                {
                    Exitoso = false,
                    Mensaje = $"Error al obtener registro de empleo: {ex.Message}",
                    CodigoEstado = 500
                };
            }
        }
    }
}
