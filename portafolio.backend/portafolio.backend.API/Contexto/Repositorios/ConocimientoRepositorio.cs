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
        public async Task<List<Conocimiento>> ObtenerConocimientosPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            return await _ctx.Conocimientos
                .Where(c => c.UsuarioAdministradorId == usuarioAdministradorId)
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

        public async Task<Conocimiento?> ObtenerConocimientoPorIdYUsuarioAdministradorIdAsync(int id, int usuarioAdministradorId)
        {
            return await _ctx.Conocimientos
                .FirstOrDefaultAsync(c => c.Id == id && c.UsuarioAdministradorId == usuarioAdministradorId);
        }

        // Método nuevo para insertar un conocimiento
        public async Task<Conocimiento> InsertarConocimientoAsync(Conocimiento conocimiento)
        {
            await _ctx.Conocimientos.AddAsync(conocimiento);
            await _ctx.SaveChangesAsync();
            return conocimiento;
        }

        // Verificar si el conocimiento tiene proyectos asociados
        public async Task<bool> TieneProyectosAsociadosAsync(int conocimientoId)
        {
            var conocimiento = await _ctx.Conocimientos
                .Include(c => c.Proyectos)
                .FirstOrDefaultAsync(c => c.Id == conocimientoId);
            
            return conocimiento != null && conocimiento.Proyectos.Any();
        }

        // Eliminar un conocimiento
        public async Task<bool> EliminarConocimientoAsync(int conocimientoId)
        {
            var conocimiento = await _ctx.Conocimientos.FindAsync(conocimientoId);
            if (conocimiento == null)
            {
                return false;
            }
            
            _ctx.Conocimientos.Remove(conocimiento);
            await _ctx.SaveChangesAsync();
            return true;
        }
    }
}
