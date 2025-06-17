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

        public async Task<List<RedSocialContacto>> ObtenerRedesSocialesPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            return await _ctx.RedesSocialesContacto
                .Where(r => r.UsuarioAdministradorId == usuarioAdministradorId)
                .ToListAsync();
        }

        public async Task<RedSocialContacto?> ObtenerRedSocialPorIdYUsuarioAdministradorIdAsync(int id, int usuarioAdministradorId)
        {
            return await _ctx.RedesSocialesContacto
                .FirstOrDefaultAsync(r => r.Id == id && r.UsuarioAdministradorId == usuarioAdministradorId);
        }

        // Método para insertar una nueva red social
        public async Task<RedSocialContacto> InsertarRedSocialContactoAsync(RedSocialContacto redSocial)
        {
            await _ctx.RedesSocialesContacto.AddAsync(redSocial);
            await _ctx.SaveChangesAsync();
            return redSocial;
        }

        // Método para eliminar una red social
        public async Task<bool> EliminarRedSocialContactoAsync(int redSocialId)
        {
            try
            {
                var redSocial = await _ctx.RedesSocialesContacto.FindAsync(redSocialId);
                if (redSocial == null)
                {
                    return false;
                }
                
                _ctx.RedesSocialesContacto.Remove(redSocial);
                await _ctx.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
