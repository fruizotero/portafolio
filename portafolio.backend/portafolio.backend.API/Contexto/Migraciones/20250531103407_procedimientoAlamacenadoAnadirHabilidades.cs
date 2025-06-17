using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace portafolio.backend.API.Contexto.Migraciones
{
    /// <inheritdoc />
    public partial class procedimientoAlamacenadoAnadirHabilidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Procedimiento almacenado para añadir habilidades iniciales
            migrationBuilder.Sql($@"
                CREATE PROCEDURE AnadirHabilidadesIniciales 
                AS
                BEGIN
                    SET NOCOUNT ON;
                    
                    -- Habilidades actuales
                    INSERT INTO Habilidades (UsuarioAdministradorId, Nombre, LogoUrl, EsActual, CreatedAt, UpdatedAt)
                    VALUES 
                        (1, 'HTML', 'https://cdn.jsdelivr.net/gh/devicons/devicon/icons/html5/html5-original.svg', 1, GETDATE(), GETDATE()),
                        (1, 'CSS', 'https://cdn.jsdelivr.net/gh/devicons/devicon/icons/css3/css3-original.svg', 1, GETDATE(), GETDATE()),
                        (1, 'JavaScript', 'https://cdn.jsdelivr.net/gh/devicons/devicon/icons/javascript/javascript-original.svg', 1, GETDATE(), GETDATE()),
                        (1, 'React', 'https://cdn.jsdelivr.net/gh/devicons/devicon/icons/react/react-original.svg', 1, GETDATE(), GETDATE()),
                        (1, 'C#', 'https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg', 1, GETDATE(), GETDATE()),
                        (1, 'PHP', 'https://cdn.jsdelivr.net/gh/devicons/devicon/icons/php/php-original.svg', 1, GETDATE(), GETDATE());
                        
                    -- Habilidades anteriores o en progreso
                    INSERT INTO Habilidades (UsuarioAdministradorId, Nombre, LogoUrl, EsActual, CreatedAt, UpdatedAt)
                    VALUES 
                        (1, 'Java', 'https://cdn.jsdelivr.net/gh/devicons/devicon/icons/java/java-original.svg', 0, GETDATE(), GETDATE()),
                        (1, 'Python', 'https://cdn.jsdelivr.net/gh/devicons/devicon/icons/python/python-original.svg', 0, GETDATE(), GETDATE());
                END
            ");

            // Invocar el procedimiento almacenado
            migrationBuilder.Sql("EXEC AnadirHabilidadesIniciales");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar el procedimiento almacenado
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS AnadirHabilidadesIniciales");

        }
    }
}
