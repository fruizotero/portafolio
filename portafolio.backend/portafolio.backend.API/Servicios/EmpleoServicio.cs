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
                // Verificar si el usuario existe
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
                        Mensaje = "No se encontraron empleos para este usuario",
                        CodigoEstado = 200,
                        Datos = new List<EmpleoResponseDTO>()
                    };
                }

                var empleosDTO = empleos.Select(MapearEmpleoADTO);

                return new ApiResponseDTO<IEnumerable<EmpleoResponseDTO>>
                {
                    Exitoso = true,
                    Mensaje = "Empleos obtenidos correctamente",
                    Datos = empleosDTO,
                    CodigoEstado = 200
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<IEnumerable<EmpleoResponseDTO>>
                {
                    Exitoso = false,
                    Mensaje = $"Error al obtener empleos: {ex.Message}",
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

                var empleoDTO = MapearEmpleoADTO(empleo);

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

        // Método para crear un nuevo empleo
        public async Task<ApiResponseDTO<EmpleoResponseDTO>> CrearEmpleoAsync(int usuarioAdministradorId, EmpleoRequestDTO empleoRequest)
        {
            try
            {
                // Verificar si el usuario existe
                var usuario = await _usuariosRepositorio.ObtenerUsuarioAdministradorPorIdAsync(usuarioAdministradorId);
                if (usuario == null)
                {
                    return new ApiResponseDTO<EmpleoResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "Usuario administrador no encontrado",
                        CodigoEstado = 404
                    };
                }

                // Validar campos obligatorios
                if (string.IsNullOrWhiteSpace(empleoRequest.Empresa))
                {
                    return new ApiResponseDTO<EmpleoResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "El nombre de la empresa es obligatorio",
                        CodigoEstado = 400
                    };
                }

                if (string.IsNullOrWhiteSpace(empleoRequest.Cargo))
                {
                    return new ApiResponseDTO<EmpleoResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "El cargo es obligatorio",
                        CodigoEstado = 400
                    };
                }

                // Validar fechas
                if (empleoRequest.FechaInicio > DateTime.Now)
                {
                    return new ApiResponseDTO<EmpleoResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "La fecha de inicio no puede ser futura",
                        CodigoEstado = 400
                    };
                }

                if (empleoRequest.FechaFin.HasValue && empleoRequest.FechaFin.Value < empleoRequest.FechaInicio)
                {
                    return new ApiResponseDTO<EmpleoResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "La fecha de finalización no puede ser anterior a la fecha de inicio",
                        CodigoEstado = 400
                    };
                }

                // Verificar si ya existe un empleo con la misma empresa y cargo para este usuario
                var empleosExistentes = await _empleoRepositorio.ObtenerEmpleosPorUsuarioAdministradorIdAsync(usuarioAdministradorId);
                if (empleosExistentes.Any(e => 
                    e.Empresa.Trim().Equals(empleoRequest.Empresa.Trim(), StringComparison.OrdinalIgnoreCase) && 
                    e.Cargo.Trim().Equals(empleoRequest.Cargo.Trim(), StringComparison.OrdinalIgnoreCase)))
                {
                    return new ApiResponseDTO<EmpleoResponseDTO>
                    {
                        Exitoso = false,
                        Mensaje = "Ya existe un empleo con esta empresa y cargo",
                        CodigoEstado = 409 // Conflict
                    };
                }

                // Crear nuevo empleo
                var nuevoEmpleo = new Empleo
                {
                    Empresa = empleoRequest.Empresa.Trim(),
                    Cargo = empleoRequest.Cargo.Trim(),
                    FechaInicio = empleoRequest.FechaInicio,
                    FechaFin = empleoRequest.FechaFin,
                    Descripcion = empleoRequest.Descripcion?.Trim(),
                    UsuarioAdministradorId = usuarioAdministradorId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                // Guardar en base de datos
                var empleoCreado = await _empleoRepositorio.InsertarEmpleoAsync(nuevoEmpleo);

                // Mapear a DTO de respuesta
                var empleoDTO = MapearEmpleoADTO(empleoCreado);

                return new ApiResponseDTO<EmpleoResponseDTO>
                {
                    Exitoso = true,
                    Mensaje = "Empleo creado correctamente",
                    Datos = empleoDTO,
                    CodigoEstado = 201 // Created
                };
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<EmpleoResponseDTO>
                {
                    Exitoso = false,
                    Mensaje = $"Error al crear empleo: {ex.Message}",
                    CodigoEstado = 500
                };
            }
        }

        private EmpleoResponseDTO MapearEmpleoADTO(Empleo empleo)
        {
            return new EmpleoResponseDTO
            {
                Id = empleo.Id,
                Empresa = empleo.Empresa,
                Cargo = empleo.Cargo,
                FechaInicio = empleo.FechaInicio,
                FechaFin = empleo.FechaFin,
                Descripcion = empleo.Descripcion
            };
        }
    }
}
