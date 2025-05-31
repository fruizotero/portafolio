using Microsoft.EntityFrameworkCore;
using portafolio.backend.API.Dominio.Entidades;

namespace portafolio.backend.API.Contexto.Repositorios
{
    public class PerfilRespositorio
    {
        private readonly ContextoPortafolio _context;
        public PerfilRespositorio(ContextoPortafolio context)
        {
            _context = context;
        }
        public async Task<Perfil> ObtenerPerfilPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            var perfilEncontrado = await _context.Perfiles
                .Where(p => p.UsuarioAdministradorId == usuarioAdministradorId).FirstOrDefaultAsync();

            return perfilEncontrado;
        }
        public async Task AñadirPerfilAsync(Perfil perfil)
        {
            await _context.Perfiles.AddAsync(perfil);
            await _context.SaveChangesAsync();
        }
        public async Task ActualizarPerfilAsync(Perfil perfil)
        {
            _context.Perfiles.Update(perfil);
            await _context.SaveChangesAsync();
        }
    }
}
