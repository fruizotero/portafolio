// Contexto/Repositorios/EmpleoRepositorio.cs
using Microsoft.EntityFrameworkCore;
using portafolio.backend.API.Dominio.Entidades;

namespace portafolio.backend.API.Contexto.Repositorios
{
    public class EmpleoRepositorio
    {
        private readonly ContextoPortafolio _ctx;

        public EmpleoRepositorio(ContextoPortafolio ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Empleo>> ObtenerPorUsuarioAsync(int usuarioAdministradorId)
        {
            return await _ctx.Empleos
                .Where(e => e.UsuarioAdministradorId == usuarioAdministradorId)
                .OrderByDescending(e => e.FechaInicio)
                .ToListAsync();
        }

        public async Task<Empleo?> ObtenerPorIdAsync(int id, int usuarioAdministradorId)
        {
            return await _ctx.Empleos
                .FirstOrDefaultAsync(e => e.Id == id && e.UsuarioAdministradorId == usuarioAdministradorId);
        }
    }
}
