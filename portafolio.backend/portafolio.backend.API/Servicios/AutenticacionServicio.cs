using portafolio.backend.API.Contexto.Repositorios;
using portafolio.backend.API.Dominio.DTOs;
using portafolio.backend.API.Dominio.DTOs.Autenticacion;
using portafolio.backend.API.Utilidades;

namespace portafolio.backend.API.Servicios
{
    public class AutenticacionServicio
    {

        private readonly UsuariosAdministradoresRepositorio _usuariosAdministradoresRepositorio;
        private readonly JWTHelper _jWTHelper;


        public AutenticacionServicio(UsuariosAdministradoresRepositorio usuariosAdministradoresRepositorio, JWTHelper jWTHelper)
        {
            _usuariosAdministradoresRepositorio = usuariosAdministradoresRepositorio;
            _jWTHelper = jWTHelper;
        }

        public async Task<ApiResponseDTO<AutenticacionResponseDTO>> Login(string email, string password)
        {
            var response = new ApiResponseDTO<AutenticacionResponseDTO>
            {
                Exitoso = false,
                Mensaje = "Credenciales inválidas",
                CodigoEstado = 400
            };


            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                response.Mensaje = "El nombre de usuario y la contraseña son obligatorios.";
                response.CodigoEstado = 400; // Bad Request
                return response;
            }

            var usuarioEncontrado = await _usuariosAdministradoresRepositorio.ObtenerUsuarioAdministradorPorEmailAsync(email);

            if (usuarioEncontrado == null)
            {
                response.Mensaje = "Usuario no encontrado";
                response.CodigoEstado = 404; // Not Found
                return response;

            }
            if (!PasswordHelper.ComprobarPassword(password, usuarioEncontrado.Password))
            {
                response.Mensaje = "Contraseña incorrecta";
                response.CodigoEstado = 401; // Unauthorized
                return response;
            }


            var token = _jWTHelper.GenerateToken(usuarioEncontrado);
            response.Exitoso = true;
            response.Mensaje = "Inicio de sesión exitoso";
            response.Datos = new AutenticacionResponseDTO
            {
                Token = token,
                Email = usuarioEncontrado.Email,
                UsuarioId = usuarioEncontrado.Id.ToString()
            };
            response.CodigoEstado = 200;
            return response;
        }
    }
}
