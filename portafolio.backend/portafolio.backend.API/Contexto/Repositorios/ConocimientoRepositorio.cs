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
        public async Task<List<Conocimiento>> ObtenerConocimientosPorUsuarioAdministradorIdAsync(int UsuarioAdministradorId)
        {
            return await _ctx.Conocimientos
                .Where(c => c.UsuarioAdministradorId == UsuarioAdministradorId)
                .ToListAsync();
        }


        public async Task<List<Conocimiento>> ObtenerTodosLosConocimientosAsync()
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
