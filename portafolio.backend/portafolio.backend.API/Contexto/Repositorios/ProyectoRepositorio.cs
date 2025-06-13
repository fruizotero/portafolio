using Microsoft.EntityFrameworkCore;
using portafolio.backend.API.Dominio.Entidades;

namespace portafolio.backend.API.Contexto.Repositorios
{
    public class ProyectoRepositorio
    {
        private readonly ContextoPortafolio _ctx;
        
        public ProyectoRepositorio(ContextoPortafolio ctx)
        {
            _ctx = ctx;
        }
        
        public async Task<List<Proyecto>> ObtenerProyectosPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            return await _ctx.Proyectos
                .Where(p => p.UsuarioAdministradorId == usuarioAdministradorId)
                .Include(p=>p.Conocimientos)
                .Include(p => p.Habilidades)
                .OrderByDescending(p => p.Orden)
                .ThenByDescending(p => p.UpdatedAt)
                .ToListAsync();
        }
        
        public async Task<Proyecto?> ObtenerProyectoPorIdYUsuarioAdministradorIdAsync(int id, int usuarioAdministradorId)
        {
            return await _ctx.Proyectos
                .Where(p => p.Id == id && p.UsuarioAdministradorId == usuarioAdministradorId)
                .Include(p => p.Conocimientos)
                .Include(p => p.Habilidades)
                .FirstOrDefaultAsync()
                ;
        }
        
        public async Task<List<Proyecto>> ObtenerProyectosDestacadosPorUsuarioAdministradorIdAsync(int usuarioAdministradorId, int cantidad = 3)
        {
            return await _ctx.Proyectos
                .Where(p => p.UsuarioAdministradorId == usuarioAdministradorId)
                .Include(p => p.Conocimientos)
                .Include(p => p.Habilidades)
                .OrderByDescending(p => p.Orden)
                .ThenByDescending(p => p.UpdatedAt)
                .Take(cantidad)
                .ToListAsync();
        }

        // Método para insertar un nuevo proyecto
        public async Task<Proyecto> InsertarProyectoAsync(Proyecto proyecto)
        {
            await _ctx.Proyectos.AddAsync(proyecto);
            await _ctx.SaveChangesAsync();
            return proyecto;
        }

        // Método para cargar un proyecto completo con sus relaciones
        public async Task<Proyecto?> ObtenerProyectoCompletoConRelacionesPorIdAsync(int id)
        {
            return await _ctx.Proyectos
                .Include(p => p.Habilidades)
                .Include(p => p.Conocimientos)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        // Eliminar un proyecto
        public async Task<bool> EliminarProyectoAsync(Proyecto proyecto)
        {
            try
            {
                // Eliminar relaciones con habilidades y conocimientos
                proyecto.Habilidades.Clear();
                proyecto.Conocimientos.Clear();
                
                _ctx.Proyectos.Remove(proyecto);
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
