using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace portafolio.backend.API.Contexto.Migraciones
{
    /// <inheritdoc />
    public partial class procedimientoAlmacenadoAnadirEmpleosIniciales : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Procedimiento almacenado para añadir empleos iniciales
            migrationBuilder.Sql($@"
                CREATE PROCEDURE AnadirEmpleosIniciales 
                AS
                BEGIN
                    SET NOCOUNT ON;
                    
                    -- Empleos iniciales para el usuario 1
                    INSERT INTO Empleos (UsuarioAdministradorId, Empresa, Cargo, FechaInicio, FechaFin, Descripcion, CreatedAt, UpdatedAt)
                    VALUES 
                        (1, 
                        'TechSolutions Inc.', 
                        'Desarrollador Full Stack Senior', 
                        '2019-03-15', 
                        NULL, 
                        'Desarrollo y mantenimiento de aplicaciones web empresariales utilizando tecnologías .NET y React. Implementación de arquitecturas escalables y optimización de rendimiento. Liderazgo técnico en equipos ágiles.',
                        GETDATE(), 
                        GETDATE()),
                        
                        (1, 
                        'InnovaSoft', 
                        'Desarrollador Frontend', 
                        '2017-06-01', 
                        '2019-02-28', 
                        'Creación de interfaces de usuario interactivas y responsivas con React, HTML5, CSS3 y JavaScript. Integración con APIs RESTful y optimización para dispositivos móviles.',
                        GETDATE(), 
                        GETDATE()),
                        
                        (1, 
                        'DataCore Systems', 
                        'Desarrollador Backend Junior', 
                        '2015-09-01', 
                        '2017-05-15', 
                        'Desarrollo de servicios web y APIs usando ASP.NET Core y Entity Framework. Implementación de autenticación y autorización con JWT. Optimización de consultas SQL.',
                        GETDATE(), 
                        GETDATE()),
                        
                        (1, 
                        'WebPro Agency', 
                        'Pasante de Desarrollo', 
                        '2014-03-01', 
                        '2015-08-15', 
                        'Colaboración en proyectos web utilizando PHP y MySQL. Aprendizaje práctico de metodologías de desarrollo y trabajo en equipo.',
                        GETDATE(), 
                        GETDATE());
                END
            ");

            // Invocar el procedimiento almacenado
            migrationBuilder.Sql("EXEC AnadirEmpleosIniciales");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar el procedimiento almacenado
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS AnadirEmpleosIniciales");

            // Eliminar datos
            migrationBuilder.Sql("DELETE FROM Empleos WHERE UsuarioAdministradorId = 1");
        }
    }
}
