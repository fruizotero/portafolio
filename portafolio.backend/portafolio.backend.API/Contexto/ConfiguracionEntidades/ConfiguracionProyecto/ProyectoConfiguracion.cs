using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using portafolio.backend.API.Dominio.Entidades;

namespace portafolio.backend.API.Contexto.ConfiguracionEntidades.ConfiguracionProyecto
{
    public class ProyectoConfiguracion : IEntityTypeConfiguration<Proyecto>
    {
        public void Configure(EntityTypeBuilder<Proyecto> builder)
        {

            builder
        .HasMany(p => p.Conocimientos)
        .WithMany(c => c.Proyectos)
        .UsingEntity<Dictionary<string, object>>(
            "ConocimientoProyecto",
            j => j.HasOne<Conocimiento>()
                  .WithMany()
                  .HasForeignKey("ConocimientosId")
                  .OnDelete(DeleteBehavior.Restrict),
            j => j.HasOne<Proyecto>()
                  .WithMany()
                  .HasForeignKey("ProyectosId")
                  .OnDelete(DeleteBehavior.Restrict),
            j =>
            {
                j.HasKey("ConocimientosId", "ProyectosId");
                j.ToTable("ConocimientoProyecto");
            });

            // Configura HabilidadProyecto (también con Restrict en Proyecto)
            builder                
                .HasMany(p => p.Habilidades)
                .WithMany(h => h.Proyectos)
                .UsingEntity<Dictionary<string, object>>(
                    "HabilidadProyecto",
                    j => j.HasOne<Habilidad>()
                          .WithMany()
                          .HasForeignKey("HabilidadesId")
                          .OnDelete(DeleteBehavior.Cascade),
                    j => j.HasOne<Proyecto>()
                          .WithMany()
                          .HasForeignKey("ProyectosId")
                          .OnDelete(DeleteBehavior.Restrict),
                    j =>
                    {
                        j.HasKey("HabilidadesId", "ProyectosId");
                        j.ToTable("HabilidadProyecto");
                    });



        }
    }
}
