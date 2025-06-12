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
    }
}
