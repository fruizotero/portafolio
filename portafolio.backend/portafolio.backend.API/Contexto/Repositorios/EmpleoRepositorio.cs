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

        public async Task<List<Empleo>> ObtenerEmpleosPorUsuarioAdministradorIdAsync(int usuarioAdministradorId)
        {
            return await _ctx.Empleos
                .Where(e => e.UsuarioAdministradorId == usuarioAdministradorId)
                .OrderByDescending(e => e.FechaInicio)
                .ToListAsync();
        }

        public async Task<Empleo?> ObtenerEmpleoPorIdYUsuarioAdministradorIdAsync(int id, int usuarioAdministradorId)
        {
            return await _ctx.Empleos
                .FirstOrDefaultAsync(e => e.Id == id && e.UsuarioAdministradorId == usuarioAdministradorId);
        }

        // Método para insertar un nuevo empleo
        public async Task<Empleo> InsertarEmpleoAsync(Empleo empleo)
        {
            await _ctx.Empleos.AddAsync(empleo);
            await _ctx.SaveChangesAsync();
            return empleo;
        }
    }
}
