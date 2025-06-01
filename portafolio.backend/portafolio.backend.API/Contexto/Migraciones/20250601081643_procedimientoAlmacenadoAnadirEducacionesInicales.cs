using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace portafolio.backend.API.Contexto.Migraciones
{
    /// <inheritdoc />
    public partial class procedimientoAlmacenadoAnadirEducacionesInicales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Procedimiento almacenado para añadir educaciones iniciales
            migrationBuilder.Sql($@"
                CREATE PROCEDURE AnadirEducacionesIniciales 
                AS
                BEGIN
                    SET NOCOUNT ON;
                    
                    -- Educaciones iniciales para el usuario 1
                    INSERT INTO Educaciones (UsuarioAdministradorId, Institucion, Titulo, FechaInicio, FechaFin, Descripcion, CreatedAt, UpdatedAt)
                    VALUES 
                        (1, 
                        'Universidad Nacional', 
                        'Ingeniería de Software', 
                        '2015-09-01', 
                        '2020-06-30', 
                        'Especialización en desarrollo web y aplicaciones móviles. Proyecto final: Plataforma de gestión educativa.',
                        GETDATE(), 
                        GETDATE()),
                        
                        (1, 
                        'Academia de Desarrollo Web', 
                        'Certificación en Desarrollo Full Stack', 
                        '2021-01-15', 
                        '2021-06-30', 
                        'Formación intensiva en tecnologías frontend y backend: React, Node.js, .NET Core.',
                        GETDATE(), 
                        GETDATE()),
                        
                        (1, 
                        'Instituto Tecnológico Superior', 
                        'Curso de Especialización en DevOps', 
                        '2022-03-10', 
                        NULL, 
                        'Actualmente cursando especialización en metodologías DevOps, CI/CD, y herramientas de automatización.',
                        GETDATE(), 
                        GETDATE());
                END
            ");

            // Invocar el procedimiento almacenado
            migrationBuilder.Sql("EXEC AnadirEducacionesIniciales");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Eliminar el procedimiento almacenado
            migrationBuilder.Sql("DROP PROCEDURE IF EXISTS AnadirEducacionesIniciales");

            // Eliminar datos
            migrationBuilder.Sql("DELETE FROM Educaciones WHERE UsuarioAdministradorId = 1");
        }
    }
}
