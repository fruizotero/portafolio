using Microsoft.EntityFrameworkCore;
using portafolio.backend.API.Dominio.Entidades;

namespace portafolio.backend.API.Contexto.Repositorios
{
    public class ConocimientoRepositorio
    {
        private readonly ContextoPortafolio _ctx;

        public ConocimientoRepositorio(ContextoPortafolio ctx)
        {
            _ctx = ctx;
        }

        // Obtener conocimientos por id de usuario
        public async Task<List<Conocimiento>> ObtenerConocimientosPorUsuarioAsync(int usuarioId)
        {
            return await _ctx.Conocimientos
                .Where(c => c.UsuarioAdministradorId == usuarioId)
                .ToListAsync();
        }


        public async Task<List<Conocimiento>> ObtenerConocimientosAsync()
        {
            return await _ctx.Conocimientos.ToListAsync();
        }

        public async Task<Conocimiento?> ObtenerConocimientoPorIdAsync(int id)
        {
            return await _ctx.Conocimientos
                            .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
