// Contexto/Repositorios/EducacionRepositorio.cs
using Microsoft.EntityFrameworkCore;
using portafolio.backend.API.Dominio.Entidades;

namespace portafolio.backend.API.Contexto.Repositorios
{
    public class EducacionRepositorio
    {
        private readonly ContextoPortafolio _ctx;

        public EducacionRepositorio(ContextoPortafolio ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Educacion>> ObtenerPorUsuarioAsync(int usuarioAdministradorId)
        {
            return await _ctx.Educaciones
                .Where(e => e.UsuarioAdministradorId == usuarioAdministradorId)
                .OrderByDescending(e => e.FechaInicio)
                .ToListAsync();
        }

        public async Task<Educacion?> ObtenerPorIdAsync(int id, int usuarioAdministradorId)
        {
            return await _ctx.Educaciones
                .FirstOrDefaultAsync(e => e.Id == id && e.UsuarioAdministradorId == usuarioAdministradorId);
        }

  

      
    }
}
