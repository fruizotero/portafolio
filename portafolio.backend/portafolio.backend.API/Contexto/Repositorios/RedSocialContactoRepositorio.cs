// Contexto/Repositorios/RedSocialContactoRepositorio.cs
using Microsoft.EntityFrameworkCore;
using portafolio.backend.API.Dominio.Entidades;

namespace portafolio.backend.API.Contexto.Repositorios
{
    public class RedSocialContactoRepositorio
    {
        private readonly ContextoPortafolio _ctx;

        public RedSocialContactoRepositorio(ContextoPortafolio ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<RedSocialContacto>> ObtenerPorUsuarioAsync(int usuarioAdministradorId)
        {
            return await _ctx.RedesSocialesContacto
                .Where(r => r.UsuarioAdministradorId == usuarioAdministradorId)
                .OrderBy(r => r.Plataforma)
                .ToListAsync();
        }

        public async Task<RedSocialContacto?> ObtenerPorIdAsync(int id, int usuarioAdministradorId)
        {
            return await _ctx.RedesSocialesContacto
                .FirstOrDefaultAsync(r => r.Id == id && r.UsuarioAdministradorId == usuarioAdministradorId);
        }
    }
}
