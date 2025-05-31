using System.Reflection;
using Microsoft.EntityFrameworkCore;
using portafolio.backend.API.Dominio.Entidades;

namespace portafolio.backend.API.Contexto
{
    
    public class ContextoPortafolio: DbContext
    {
        public DbSet<UsuarioAdministrador> UsuariosAdministradores { get; set; }
        public DbSet<Perfil> Perfiles { get; set; }
        public DbSet<Habilidad> Habilidades { get; set; } = null!;

        public ContextoPortafolio(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
