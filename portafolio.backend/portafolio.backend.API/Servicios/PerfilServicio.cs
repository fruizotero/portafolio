using portafolio.backend.API.Contexto.Repositorios;
using portafolio.backend.API.Dominio.DTOs;
using portafolio.backend.API.Dominio.DTOs.Imagen;
using portafolio.backend.API.Dominio.DTOs.Perfil;
using portafolio.backend.API.Dominio.Entidades;
using portafolio.backend.API.Interfaces;

namespace portafolio.backend.API.Servicios
{
    public class PerfilServicio
    {
        private readonly PerfilRespositorio _perfilRepositorio;
        private readonly ServicioImagenes _servicioImagenes;
        public PerfilServicio(PerfilRespositorio perfilRepositorio, ServicioImagenes servicioImagenes)
        {
            _perfilRepositorio = perfilRepositorio ?? throw new ArgumentNullException(nameof(perfilRepositorio));
            _servicioImagenes = servicioImagenes ?? throw new ArgumentNullException(nameof(servicioImagenes));
        }
        public async Task<ApiResponseDTO<PerfilResponseDTO>> ObtenerPerfilPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
          var response = new ApiResponseDTO<PerfilResponseDTO>
          {
              Exitoso = false,
              Mensaje = "Perfil no encontrado",
              CodigoEstado = 404 // Not Found
          };

            var perfilEncontrado = await _perfilRepositorio.ObtenerPerfilPorUsuarioAdministradorIdAsync(usuarioAdministradorId);
            if (perfilEncontrado != null)
            {
                response.Exitoso = true;
                response.Mensaje = "Perfil encontrado";
                response.Datos = new PerfilResponseDTO
                {
                    AcercaDeMi = perfilEncontrado.AcercaDeMi,
                    Apellidos = perfilEncontrado.Apellidos,
                    Descripcion = perfilEncontrado.Descripcion,
                    FotoURL = perfilEncontrado.FotoURL,
                    Nombre = perfilEncontrado.Nombre,
                    Saludo = perfilEncontrado.Saludo
                };
                response.CodigoEstado = 200; // OK

            }



                return response;
        }

        public async Task<ApiResponseDTO<string>> CrearOActualizarPerfilAsync(int usuarioAdministradorId, PerfilRequestDTO perfilRequest)
        {
            try
            {
                var perfilExistente = await _perfilRepositorio.ObtenerPerfilPorUsuarioAdministradorIdAsync(usuarioAdministradorId);

                if (perfilExistente == null)
                {
                    // Crear nuevo perfil
                    var nuevoPerfil = new Perfil
                    {
                        UsuarioAdministradorId = usuarioAdministradorId,
                        Nombre = perfilRequest.Nombre,
                        Apellidos = perfilRequest.Apellidos,
                        Saludo = perfilRequest.Saludo,
                        Descripcion = perfilRequest.Descripcion,
                        AcercaDeMi = perfilRequest.AcercaDeMi,
                        FotoURL = await CrearFotoUrl(perfilRequest.Foto),
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };

                    await _perfilRepositorio.CrearPerfilAsync(nuevoPerfil);

                    return new ApiResponseDTO<string>
                    {
                        Exitoso = true,
                        Mensaje = "Perfil creado correctamente",
                        CodigoEstado = 201 // Created
                    };
                }
                else
                {
                    // Actualizar perfil existente
                    perfilExistente.Nombre = perfilRequest.Nombre;
                    perfilExistente.Apellidos = perfilRequest.Apellidos;
                    perfilExistente.Saludo = perfilRequest.Saludo;
                    perfilExistente.Descripcion = perfilRequest.Descripcion;
                    perfilExistente.AcercaDeMi = perfilRequest.AcercaDeMi;
                    perfilExistente.UpdatedAt = DateTime.UtcNow;

                    if(perfilRequest.Foto != null)
                    {
                        perfilExistente.FotoURL = await CrearFotoUrl(perfilRequest.Foto);
                    }

                    await _perfilRepositorio.ActualizarPerfilAsync(perfilExistente);

                    return new ApiResponseDTO<string>
                    {
                        Exitoso = true,
                        Mensaje = "Perfil actualizado correctamente",
                        CodigoEstado = 200 // OK
                    };
                }
            }
            catch (Exception ex)
            {
                return new ApiResponseDTO<string>
                {
                    Exitoso = false,
                    Mensaje = $"Error al crear o actualizar el perfil: {ex.Message}",
                    CodigoEstado = 500 // Internal Server Error
                };
            }
        }

        private async Task< string?> CrearFotoUrl(IFormFile file )
        {
            ImagenUploadRequest imagenUploadRequest = new ImagenUploadRequest(file);
            var foto = await _servicioImagenes.SubirImagenAsync(imagenUploadRequest);

            return foto.Url;

        }

        private PerfilResponseDTO MapearPerfilADTO(Perfil perfil)
        {
            return new PerfilResponseDTO
            {
                Nombre = perfil.Nombre,
                Apellidos = perfil.Apellidos,
                Saludo = perfil.Saludo,
                Descripcion = perfil.Descripcion,
                AcercaDeMi = perfil.AcercaDeMi,
                FotoURL = perfil.FotoURL
            };
        }
    }
}
