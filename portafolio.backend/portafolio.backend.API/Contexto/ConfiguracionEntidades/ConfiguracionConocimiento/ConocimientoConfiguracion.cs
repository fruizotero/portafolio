using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using portafolio.backend.API.Dominio.Entidades;

namespace portafolio.backend.API.Contexto.ConfiguracionEntidades.ConfiguracionConocimiento
{
    public class ConocimientoConfiguracion : IEntityTypeConfiguration<Conocimiento>
    {
        public void Configure(EntityTypeBuilder<Conocimiento> builder)
        {
          
        }
    }
}
