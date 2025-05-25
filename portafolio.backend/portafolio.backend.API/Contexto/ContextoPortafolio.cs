using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace portafolio.backend.API.Contexto
{
    
    public class ContextoPortafolio: DbContext
    {
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
