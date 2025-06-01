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
        
        public async Task<List<Proyecto>> ObtenerPorUsuario(int usuarioAdministradorId)
        {
            return await _ctx.Proyectos
                .Where(p => p.UsuarioAdministradorId == usuarioAdministradorId)
                .Include(p=>p.Conocimientos)
                .Include(p => p.Habilidades)
                .OrderByDescending(p => p.Orden)
                .ThenByDescending(p => p.UpdatedAt)
                .ToListAsync();
        }
        
        public async Task<Proyecto?> ObtenerPorId(int id, int usuarioAdministradorId)
        {
            return await _ctx.Proyectos
                .Where(p => p.Id == id && p.UsuarioAdministradorId == usuarioAdministradorId)
                .Include(p => p.Conocimientos)
                .Include(p => p.Habilidades)
                .FirstOrDefaultAsync()
                ;
        }
        
        public async Task<List<Proyecto>> ObtenerDestacados(int usuarioAdministradorId, int cantidad = 3)
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
    }
}
