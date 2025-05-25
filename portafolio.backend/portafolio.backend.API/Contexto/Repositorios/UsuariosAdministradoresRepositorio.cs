using Microsoft.EntityFrameworkCore;
using portafolio.backend.API.Dominio.Entidades;

namespace portafolio.backend.API.Contexto.Repositorios
{
    public class UsuariosAdministradoresRepositorio
    {

        private readonly ContextoPortafolio _contexto;

        public UsuariosAdministradoresRepositorio(ContextoPortafolio contexto)
        {
            _contexto = contexto ?? throw new ArgumentNullException(nameof(contexto));
        }


        public async Task<bool> ExisteUsuarioAdministrador(string email)
        {
            var usuarioEncontrado = await _contexto.UsuariosAdministradores.Where(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)).FirstOrDefaultAsync();
            return usuarioEncontrado != null;
        }

        public async Task<UsuarioAdministrador> ObtenerUsuarioAdministradorPorEmailAsync(string email)
        {
            var usuarioEncontrado = await _contexto.UsuariosAdministradores.
                Where(u => u.Email.ToLower() == email.ToLower())
                .FirstOrDefaultAsync();
            return usuarioEncontrado;
        }

    }
}
