using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace portafolio.backend.API.Contexto.Migraciones
{
    /// <inheritdoc />
    public partial class procedimientoAlmacenadoAnadirRedesScoailesIniciales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Procedimiento almacenado para añadir redes sociales iniciales
            migrationBuilder.Sql($@"
                CREATE PROCEDURE AnadirRedesSocialesIniciales 
                AS
                BEGIN
                    SET NOCOUNT ON;
                    
                    -- Redes sociales iniciales para el usuario 1
                    INSERT INTO RedesSocialesContacto (UsuarioAdministradorId, Plataforma, Url, IconUrl, CreatedAt, UpdatedAt)
                    VALUES 
                        (1, 
                        'LinkedIn', 
                        'https://www.linkedin.com/in/usuario-demo/', 
                        'https://cdn.jsdelivr.net/gh/devicons/devicon/icons/linkedin/linkedin-original.svg',
                        GETDATE(), 
                        GETDATE()),
                        
                        (1, 
                        'GitHub', 
                        'https://github.com/usuario-demo', 
                        'https://cdn.jsdelivr.net/gh/devicons/devicon/icons/github/github-original.svg',
                        GETDATE(), 
                        GETDATE()),
                        
                        (1, 
                        'Twitter', 
                        'https://twitter.com/usuario_demo', 
                        'https://cdn.jsdelivr.net/gh/devicons/devicon/icons/twitter/twitter-original.svg',
                        GETDATE(), 
                        GETDATE()),
                        
                        (1, 
                        'Email', 
                        'mailto:contacto@usuario-demo.com', 
                        'https://cdn-icons-png.flaticon.com/512/552/552486.png',
                        GETDATE(), 
                        GETDATE()),
                        
                        (1, 
                        'Portfolio', 
                        'https://www.usuario-demo.com', 
                        'https://cdn-icons-png.flaticon.com/512/1454/1454827.png',
                        GETDATE(), 
                        GETDATE());
                END
            ");

            // Invocar el procedimiento almacenado
            migrationBuilder.Sql("EXEC AnadirRedesSocialesIniciales");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar el procedimiento almacenado
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS AnadirRedesSocialesIniciales");

            // Eliminar datos
            migrationBuilder.Sql("DELETE FROM RedesSocialesContacto WHERE UsuarioAdministradorId = 1");
        }
    }
}
