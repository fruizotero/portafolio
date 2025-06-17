using Microsoft.EntityFrameworkCore;
using portafolio.backend.API.Dominio.Entidades;

namespace portafolio.backend.API.Contexto.Repositorios
{
    public class HabilidadRepositorio
    {
        private readonly ContextoPortafolio _context;

        public HabilidadRepositorio(ContextoPortafolio context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Habilidad>> ObtenerHabilidadesPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            return await _context.Habilidades
                .Where(h => h.UsuarioAdministradorId == usuarioAdministradorId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Habilidad>> ObtenerHabilidadesActualesPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            return await _context.Habilidades
                .Where(h => h.UsuarioAdministradorId == usuarioAdministradorId && h.EsActual)
                .ToListAsync();
        }

        // Método para insertar una nueva habilidad
        public async Task<Habilidad> InsertarHabilidadAsync(Habilidad habilidad)
        {
            await _context.Habilidades.AddAsync(habilidad);
            await _context.SaveChangesAsync();
            return habilidad;
        }

        // obtener habilidad por id y id de usuario administador
        public async Task<Habilidad?> ObtenerHabilidadPorIdYUsuarioAdministradorIdAsync(int id, int usuarioAdministradorId)
        {
            return await _context.Habilidades
                .FirstOrDefaultAsync(h => h.Id == id && h.UsuarioAdministradorId == usuarioAdministradorId);
        }

        // Verificar si la habilidad tiene proyectos asociados
        public async Task<bool> TieneProyectosAsociadosAsync(int habilidadId)
        {
            var habilidad = await _context.Habilidades
                .Include(h => h.Proyectos)
                .FirstOrDefaultAsync(h => h.Id == habilidadId);
            
            return habilidad != null && habilidad.Proyectos.Any();
        }

        // Eliminar una habilidad
        public async Task<bool> EliminarHabilidadAsync(int habilidadId)
        {
            var habilidad = await _context.Habilidades.FindAsync(habilidadId);
            if (habilidad == null)
            {
                return false;
            }
            
            _context.Habilidades.Remove(habilidad);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
